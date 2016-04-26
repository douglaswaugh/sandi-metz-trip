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