﻿using System.Collections.Generic;

namespace ABT.Test.Lib.InstrumentDrivers {
    public enum INSTRUMENT_TYPES { ELECTRONIC_LOAD, LOGIC_ANALYZER, MULTI_FUNCTION, MULTI_METER, OSCILLOSCOPE, POWER_ANALYZER, POWER_SUPPLY, SWITCHING, UNKNOWN, WAVEFORM_GENERATOR }
    public enum INSTRUMENT_CATEGORIES { MEASURING, STIMULATING, SWITCHING, UNKNOWN }
    public enum INSTRUMENT_DRIVER_TYPES { IVI_C, IVI_COM, IVI_NET, SCPI_C, SCPI_COM, SCPI_NET }
    public enum STATES { off = 0, ON = 1 } // NOTE: To Command an instrument off or ON, and Query it's STATE, again off or ON.
    public enum SENSE_MODE { EXTernal, INTernal }
    // Consistent convention for lower-cased inactive states off/low/zero as 1st states in enums, UPPER-CASED active ON/HIGH/ONE as 2nd states.

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
