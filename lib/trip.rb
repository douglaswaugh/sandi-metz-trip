class Trip
    def initialize(bicycles)
        @bicycles = bicycles
    end

    def prepare(preparers)
        preparers.each{ |preparer| preparer.prepare_trip(self) }
    end

    def bicycles
        return @bicycles
    end
end