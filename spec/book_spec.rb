require 'spec_helper'

describe Book do
    before :each do
        @book = Book.new "Title", "Author", :category
    end

    describe "new" do 
        it "takes three parameters and returns a Book object" do
            expect(@book).to be_a Book
        end    
    end
end

