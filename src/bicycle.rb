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