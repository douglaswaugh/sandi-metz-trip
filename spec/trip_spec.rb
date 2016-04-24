require 'spec_helper'

describe Trip do
	before :each do
		brakes = Brakes.new(100);
		velocity = MilesPerHour.new(0);
		@trip = Trip.new(Bicycle.new(brakes,velocity))
	end

	describe "new" do
		it "returns a Trip object" do
			expect(@trip).to be_a Trip
		end
	end
end