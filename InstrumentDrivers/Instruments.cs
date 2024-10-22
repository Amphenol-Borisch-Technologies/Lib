﻿using System.Collections.Generic;

namespace ABT.Test.Lib.InstrumentDrivers {
    public enum INSTRUMENT_TYPES { ELECTRONIC_LOAD, LOGIC_ANALYZER, MULTI_FUNCTION, MULTI_METER, OSCILLOSCOPE, POWER_ANALYZER, POWER_SUPPLY, SWITCHING, UNKNOWN, WAVEFORM_GENERATOR }
    public enum INSTRUMENT_CATEGORIES { MEASURING, STIMULATING, SWITCHING, UNKNOWN }

    public static class Instruments {
        public static Dictionary<INSTRUMENT_TYPES, INSTRUMENT_CATEGORIES> InstrumentClassification = new Dictionary<INSTRUMENT_TYPES, INSTRUMENT_CATEGORIES>() {
            { INSTRUMENT_TYPES.ELECTRONIC_LOAD, INSTRUMENT_CATEGORIES.STIMULATING },
            { INSTRUMENT_TYPES.LOGIC_ANALYZER, INSTRUMENT_CATEGORIES.MEASURING },
            { INSTRUMENT_TYPES.MULTI_FUNCTION, INSTRUMENT_CATEGORIES.UNKNOWN },
            { INSTRUMENT_TYPES.MULTI_METER, INSTRUMENT_CATEGORIES.MEASURING },
            { INSTRUMENT_TYPES.OSCILLOSCOPE, INSTRUMENT_CATEGORIES.MEASURING },
            { INSTRUMENT_TYPES.POWER_ANALYZER, INSTRUMENT_CATEGORIES.MEASURING },
            { INSTRUMENT_TYPES.POWER_SUPPLY, INSTRUMENT_CATEGORIES.STIMULATING },
            { INSTRUMENT_TYPES.SWITCHING, INSTRUMENT_CATEGORIES.UNKNOWN },
            { INSTRUMENT_TYPES.UNKNOWN, INSTRUMENT_CATEGORIES.UNKNOWN },
            { INSTRUMENT_TYPES.WAVEFORM_GENERATOR, INSTRUMENT_CATEGORIES.STIMULATING }
        };
    }
}
