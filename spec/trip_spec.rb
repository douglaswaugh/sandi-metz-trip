require 'spec_helper'

describe Trip do
	before :each do
		@trip = Trip.new
	end

	describe "new" do
		it "returns a Trip object" do
			expect(@trip).to be_a Trip
		end
	end
end