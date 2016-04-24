require 'spec_helper'

class Brakes
end

class MilesPerHour
end

class Bicycle
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
			var oldBrakes = new Brakes(0);
            var velocity = new MilesPerHour(1);
            var bicycle = new Bicycle(oldBrakes, velocity);
            var trip = new Trip(new[] {bicycle}, null, null);

            var mechanic = new Mechanic();

            mechanic.PrepareTrip(trip);

            Assert.DoesNotThrow(bicycle.ApplyBrakes);
		end
	end
end