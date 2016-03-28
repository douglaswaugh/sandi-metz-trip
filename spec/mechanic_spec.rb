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
end