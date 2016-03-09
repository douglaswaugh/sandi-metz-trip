using System.Collections.Generic;

namespace BikeTours
{
    public class Trip
    {
        public Trip(IEnumerable<Bicycle> bicycles, IEnumerable<Customer> customers, Vehicle vehicle)
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

        public IEnumerable<Bicycle> Bicycles { get; private set; }
        public IEnumerable<Customer> Customers { get; private set; }
        public Vehicle Vehicle { get; private set; }
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

    public class Bicycle
    {
        public void FixBrakes(Mechanic mechanic)
        {
            
        }
    }

    public interface Preparer
    {
        void PrepareTrip(dynamic trip);
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

        private void PrepareBicycle(Bicycle bicycle)
        {
            CleanBicycle(bicycle);
            PumpTires(bicycle);
            LubeChain(bicycle);
            CheckBrakes(bicycle);
        }

        private void CheckBrakes(Bicycle bicycle)
        {
            bicycle.FixBrakes(this);
        }

        private void LubeChain(Bicycle bicycle) { }

        private void PumpTires(Bicycle bicycle) { }

        private void CleanBicycle(Bicycle bicycle) { }
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
