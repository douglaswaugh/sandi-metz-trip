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