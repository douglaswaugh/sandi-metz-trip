require 'spec_helper'

class BrakesFailedError < RuntimeError
end

class Brakes
	def initialize(brakeApplications)
		@brakeApplications = brakeApplications
	end

	def apply(vehicle)
		if @brakeApplications > 0
			@brakeApplications = @brakeApplications - 1
			vehicle.slow
			return
		end

		raise BrakesFailedError.new
	end
end

class MilesPerHour
	def initialize(milesPerHour)
		@milesPerHour = milesPerHour
	end

	def slow
		if @milesPerHour > 0
			@milesPerHour = @milesPerHour - 1
		end
	end
end

class Bicycle
	def initialize(brakes,velocity)
		@brakes = brakes
		@velocity = velocity
	end

	def fix_brakes(brakes)
		@brakes = brakes
	end

	def apply_brakes
		@brakes.apply(self)
	end

	def slow
		@velocity.slow
	end
end

class Trip
	def initialize(bicycles)
		@bicycles = bicycles
	end

	def prepare(preparers)
		preparers.each{ |preparer| preparer.prepare_trip(self) }
	end

	def bicycles
		return @bicycles
	end
end

class Mechanic
	def prepare_trip(trip)
		trip.bicycles.each { |bicycle| prepare_bicycle(bicycle) }
	end

	def prepare_bicycle(bicycle)
		clean_bicycle(bicycle)
		check_brakes(bicycle)
	end

	def check_brakes(bicycle)
		bicycle.fix_brakes(Brakes.new(100))
	end

	def clean_bicycle(bicycle)
		# code to clean the bicycle goes here
	end
end

describe Mechanic do
	before :each do
		@mechanic = Mechanic.new
	end

	describe "new" do
		it "should return new Mechanic" do
			expect(@mechanic).to be_a Mechanic
		end
	end

	describe "prepare" do
		it "should be able to test using concrete instances" do
 			oldBrakes = Brakes.new(0)
			velocity = MilesPerHour.new(1)
            bicycle = Bicycle.new(oldBrakes, velocity)
            trip = Trip.new([bicycle])
            mechanic = Mechanic.new

            mechanic.prepare_trip(trip)

            expect {bicycle.apply_brakes}.to_not raise_error
		end

		it "should be able to test using a mock for bicycle" do
			bicycle = double("bicycle")
			expect(bicycle).to receive(:fix_brakes)
			trip = Trip.new([bicycle])
			mechanic = Mechanic.new()

			mechanic.prepare_trip(trip)
		end

		it "should be able to test using a mock for bicycle a stub for trip" do
			bicycle = double("bicycle")
			expect(bicycle).to receive(:fix_brakes)
			trip = double("trip", :bicycles => [bicycle])
			mechanic = Mechanic.new()

			mechanic.prepare_trip(trip)
		end
	end
end
