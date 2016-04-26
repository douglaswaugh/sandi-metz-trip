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