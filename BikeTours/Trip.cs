using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BikeTours
{
    public class Trip : Activity
    {
        public Trip(IEnumerable<Transport> bicycles, IEnumerable<Customer> customers, Vehicle vehicle)
        {
            Bicycles = bicycles;
            Customers = customers;
            Vehicle = vehicle;
        }

        public void Prepare(IEnumerable<Preparer> preparers)
        {
            foreach (var preparer in preparers)
            {
                preparer.PrepareTrip(this);
            }
        }

        public IEnumerable<Transport> Bicycles { get; private set; }
        public IEnumerable<Customer> Customers { get; private set; }
        public Vehicle Vehicle { get; private set; }
    }

    public interface Activity
    {
        IEnumerable<Transport> Bicycles { get; }
    }

    public class Vehicle
    {
        public void FillWaterTank(Driver driver) { }
        public void FillPetrolTank(Driver driver) { }
    }

    public class Customer
    {
        public void GiveFood(TripCoordinator tripCoordinator)
        {
            
        }
    }

    public class Bicycle : Transport
    {
        private Brakes _brakes;
        private readonly Velocity _velocity;

        public Bicycle(Brakes brakes, Velocity velocity)
        {
            _brakes = brakes;
            _velocity = velocity;
        }

        public void FixBrakes(Brakes brakes)
        {
            _brakes = brakes;
        }

        public void ApplyBrakes()
        {
            _brakes.Apply(_velocity);
        }
    }

    public interface Transport
    {
        void FixBrakes(Brakes brakes);
    }

    public interface Velocity
    {
        void Slow();
    }

    public interface Preparer
    {
        void PrepareTrip(dynamic trip);
    }

    [TestFixture]
    public class MechanicTests
    {
        [Test]
        public void Testing_concreate_instances()
        {
            var oldBrakes = new Brakes(0);
            var velocity = new MilesPerHour(1);
            var bicycle = new Bicycle(oldBrakes, velocity);
            var trip = new Trip(new[] {bicycle}, null, null);

            var mechanic = new Mechanic();

            mechanic.PrepareTrip(trip);

            Assert.DoesNotThrow(bicycle.ApplyBrakes);
        }

        [Test]
        public void Testing_the_mechanic_using_the_transport_interface_for_bicycle()
        {
            var transport = Substitute.For<Transport>();
            var trip = new Trip(new[] {transport}, null, null);

            var mechanic = new Mechanic();

            mechanic.PrepareTrip(trip);

            transport.Received().FixBrakes(Arg.Any<Brakes>());
        }

        [Test]
        public void Testing_the_mechanic_using_the_activity_and_transport_interfaces_for_trip_and_bicycle()
        {
            var transport = Substitute.For<Transport>();
            var trip = Substitute.For<Activity>();
            trip.Bicycles.Returns(new[] {transport});

            var mechanic = new Mechanic();

            mechanic.PrepareTrip(trip);

            transport.Received().FixBrakes(Arg.Any<Brakes>());
        }
    }

    public class MilesPerHour : Velocity
    {
        private int _milesPerHour;

        public MilesPerHour(int milesPerHour)
        {
            _milesPerHour = milesPerHour;
        }

        public void Slow()
        {
            if (_milesPerHour > 0)
            {
                _milesPerHour -= 1;
            }
        }
    }

    public class Brakes
    {
        private int _brakeApplications;

        public Brakes(int brakeApplications)
        {
            _brakeApplications = brakeApplications;
        }

        public void Apply(Velocity transport)
        {
            if (_brakeApplications > 0)
            {
                _brakeApplications -= 1;
                transport.Slow();
                return;
            }

            throw new BrakesFailureException();
        }
    }

    public class BrakesFailureException : Exception
    {
    }

    public class Mechanic : Preparer
    {
        public void PrepareTrip(dynamic trip)
        {
            foreach (var bicycle in trip.Bicycles)
            {
                PrepareBicycle(bicycle);
            }
        }

        private void PrepareBicycle(Transport bicycle)
        {
            CleanBicycle(bicycle);
            PumpTires(bicycle);
            LubeChain(bicycle);
            CheckBrakes(bicycle);
        }

        private void CheckBrakes(Transport bicycle)
        {
            bicycle.FixBrakes(new Brakes(100));
        }

        private void LubeChain(Transport bicycle) { }

        private void PumpTires(Transport bicycle) { }

        private void CleanBicycle(Transport bicycle) { }
    }

    public class TripCoordinator : Preparer
    {
        public void PrepareTrip(dynamic trip)
        {
            foreach (var customer in trip.Customers)
            {
                BuyFood(customer);   
            }
        }

        private void BuyFood(Customer customer)
        {
            customer.GiveFood(this);
        }
    }

    public class Driver : Preparer
    {
        public void PrepareTrip(dynamic trip)
        {
            var vehicle = trip.Vehicle;
            FillPetrolTank(vehicle);
            FillWaterTank(vehicle);
        }

        private void FillWaterTank(Vehicle vehicle)
        {
            vehicle.FillWaterTank(this);
        }

        private void FillPetrolTank(Vehicle vehicle)
        {
            vehicle.FillPetrolTank(this);
        }
    }
}
