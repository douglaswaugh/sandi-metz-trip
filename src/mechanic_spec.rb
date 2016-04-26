require 'spec_helper'

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
