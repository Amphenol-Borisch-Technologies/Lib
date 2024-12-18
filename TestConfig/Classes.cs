﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ABT.Test.TestLib.TestConfig {
    public interface IAssertionCurrent { String AssertionCurrent(); }

    [XmlRoot(nameof(TO))] public class TO : IAssertionCurrent {
        // NOTE: Constructor-less because only instantiated via System.Xml.Serialization.XmlSerializer, thus constructor unnecessary.
        [XmlAttribute(nameof(Namespace))] public String Namespace { get; set; }
        [XmlAttribute(nameof(Description))] public String Description { get; set; }
        [XmlElement(nameof(TG))] public List<TG> TestGroups { get; set; }
        internal const String DEBUG_ASSERT = "Debug.Assert(";
        internal const String BEGIN = "(";
        internal const String CS = ": ";
        internal const String CONTINUE = ", ";
        internal const String END = "));";
        internal const String DIVIDER = "|";
        internal const String NONE = "\"NONE\"";
        public String AssertionCurrent() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{DEBUG_ASSERT}{GetType().Name}{BEGIN}");
            sb.Append($"{nameof(Namespace)}{CS}{EF(GetType().GetProperty(nameof(Namespace)).GetValue(this))}{CONTINUE}");
            sb.Append($"{nameof(Description)}{CS}{EF(GetType().GetProperty(nameof(Description)).GetValue(this))}");
            sb.Append($"{CONTINUE}{nameof(TestGroups)}{CS}{TGs()}");
            sb.Append($"{END}");
            return sb.ToString();
        }
        public static String EF(Object o) {
            String s = (o.ToString()).Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'");
            return $"\"{s}\"";
        }
        private String TGs() {
            StringBuilder sb = new StringBuilder();
            foreach (TG tg in TestGroups) sb.Append($"{tg.Class}{DIVIDER}");
            return EF(sb.Remove(sb.Length - DIVIDER.Length, DIVIDER.Length).ToString()); // Remove trailing DIVIDER.
        }
    }

    public class TG : IAssertionCurrent {
        // NOTE: Constructor-less because only instantiated via System.Xml.Serialization.XmlSerializer, thus constructor unnecessary.
        [XmlAttribute(nameof(Class))] public String Class { get; set; }
        [XmlAttribute(nameof(Description))] public String Description { get; set; }
        [XmlAttribute(nameof(CancelIfFail))] public Boolean CancelIfFail { get; set; }
        [XmlAttribute(nameof(Independent))] public Boolean Independent { get; set; }
        [XmlElement(nameof(MC), typeof(MC))]
        [XmlElement(nameof(MI), typeof(MI))]
        [XmlElement(nameof(MP), typeof(MP))]
        [XmlElement(nameof(MT), typeof(MT))]
        public List<M> Methods { get; set; }
        public readonly Int32 FormattingLengthGroupID = 0;
        public readonly Int32 FormattingLengthMeasurementID = 0;
        public Statistics Statistics { get; set; } = new Statistics();

        public String AssertionPrior() { return $"{TO.DEBUG_ASSERT}{nameof(Assertions.TG_Prior)}{TO.BEGIN}{nameof(Class)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Class)).GetValue(this))}{TO.END}"; }

        public String AssertionCurrent() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{TO.DEBUG_ASSERT}{GetType().Name}{TO.BEGIN}");
            sb.Append($"{nameof(Class)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Class)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(Description)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Description)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(CancelIfFail)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(CancelIfFail)).GetValue(this).ToString().ToLower())}{TO.CONTINUE}");
            sb.Append($"{nameof(Independent)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Independent)).GetValue(this).ToString().ToLower())}");
            sb.Append($"{TO.CONTINUE}{nameof(Methods)}{TO.CS}{Ms()}");
            sb.Append($"{TO.END}");
            return sb.ToString();
        }
        private String Ms() {
            StringBuilder sb = new StringBuilder();
            foreach (M m in Methods) sb.Append($"{m.Method}{TO.DIVIDER}");
            return TO.EF(sb.Remove(sb.Length - TO.DIVIDER.Length, TO.DIVIDER.Length).ToString()); // Remove trailing TO.DIVIDER.
        }

        public String AssertionNext() { return $"{TO.DEBUG_ASSERT}{nameof(Assertions.TG_Next)}{TO.BEGIN}{nameof(Class)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Class)).GetValue(this))}{TO.END}"; }

        public String StatisticsDisplay() {
            const Int32 L = 6;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Cancelled : {Statistics.Cancelled,L}, {Statistics.PercentCancelled(),L:P1}");
            sb.AppendLine($"E-Stopped : {Statistics.EmergencyStopped,L}, {Statistics.PercentEmergencyStopped(),L:P1}");
            sb.AppendLine($"Errored   : {Statistics.Errored,L}, {Statistics.PercentErrored(),L:P1}");
            sb.AppendLine($"Failed    : {Statistics.Failed,L}, {Statistics.PercentFailed(),L:P1}");
            sb.AppendLine($"Ignored   : {Statistics.Ignored,L}, {Statistics.PercentIgnored(),L:P1}");
            sb.AppendLine($"Passed    : {Statistics.Passed,L}, {Statistics.PercentPassed(),L:P1}");
            sb.AppendLine($"------");
            sb.AppendLine($"Total     : {Statistics.Tested(),L}");
            return sb.ToString();
        }

        public String StatisticsStatus() { return $"   Failed: {Statistics.Failed}     Passed: {Statistics.Passed}   "; }

        public String StatusTime() { return $"   Time: {Statistics.Time()}"; }
    }

    public abstract class M {
        // NOTE: Constructor-less because only instantiated via System.Xml.Serialization.XmlSerializer, thus constructor unnecessary.
        [XmlAttribute(nameof(Method))] public String Method { get; set; }
        [XmlAttribute(nameof(Description))] public String Description { get; set; }
        [XmlAttribute(nameof(CancelIfFail))] public Boolean CancelIfFail { get; set; }
        [XmlAttribute(nameof(Event))] public EVENTS Event { get; set; }
        [XmlAttribute(nameof(EventDetail))] public String EventDetail { get; set; }
        public String AssertionPrior() { return $"{TO.DEBUG_ASSERT}{nameof(Assertions.M_Prior)}{TO.BEGIN}{nameof(Method)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Method)).GetValue(this))}{TO.END}"; }

        private protected String AssertionM() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{nameof(Method)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Method)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(Description)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Description)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(CancelIfFail)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(CancelIfFail)).GetValue(this).ToString().ToLower())}");
            return sb.ToString();
        }

        public String AssertionNext() { return $"{TO.DEBUG_ASSERT}{nameof(Assertions.M_Next)}{TO.BEGIN}{nameof(Method)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Method)).GetValue(this))}{TO.END}"; }
    }

    public class MC : M, IAssertionCurrent {
        [XmlElement(nameof(Parameter))] public List<Parameter> Parameters { get; set; }

        public String AssertionCurrent() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{TO.DEBUG_ASSERT}{GetType().Name}{TO.BEGIN}");
            sb.Append($"{AssertionM()}");
            if (Parameters.Count > 0) sb.Append($"{TO.CONTINUE}{nameof(Parameters)}{TO.CS}{Ps()}");
            sb.Append($"{TO.END}");
            return sb.ToString();
        }
        private String Ps() {
            StringBuilder sb = new StringBuilder();
            foreach (Parameter p in Parameters) sb.Append($"{p.Key}={p.Value}{TO.DIVIDER}");
            return TO.EF(sb.Remove(sb.Length - TO.DIVIDER.Length, TO.DIVIDER.Length).ToString()); // Remove trailing TO.DIVIDER.
        }
    }

    public class Parameter {
        // NOTE: Constructor-less because only instantiated via System.Xml.Serialization.XmlSerializer, thus constructor unnecessary.
        [XmlAttribute(nameof(Key))] public String Key { get; set; }
        [XmlAttribute(nameof(Value))] public String Value { get; set; }
    }

    public class MI : M, IAssertionCurrent {
        // NOTE: Constructor-less because only instantiated via System.Xml.Serialization.XmlSerializer, thus constructor unnecessary.
        [XmlAttribute(nameof(LowComparator))] public MI_LowComparator LowComparator { get; set; }
        [XmlAttribute(nameof(Low))] public Double Low { get; set; }
        [XmlAttribute(nameof(High))] public Double High { get; set; }
        [XmlAttribute(nameof(HighComparator))] public MI_HighComparator HighComparator { get; set; }
        [XmlAttribute(nameof(FractionalDigits))] public UInt32 FractionalDigits { get; set; }
        [XmlAttribute(nameof(UnitPrefix))] public MI_UnitPrefix UnitPrefix { get; set; }
        [XmlAttribute(nameof(Units))] public MI_Units Units { get; set; }
        [XmlAttribute(nameof(UnitSuffix))] public MI_UnitSuffix UnitSuffix { get; set; }

        public String AssertionCurrent() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{TO.DEBUG_ASSERT}{GetType().Name}{TO.BEGIN}");
            sb.Append($"{AssertionM()}{TO.CONTINUE}");
            sb.Append($"{nameof(LowComparator)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(LowComparator)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(Low)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Low)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(High)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(High)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(HighComparator)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(HighComparator)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(FractionalDigits)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(FractionalDigits)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(UnitPrefix)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(UnitPrefix)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(Units)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Units)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(UnitSuffix)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(UnitSuffix)).GetValue(this))}{TO.END}");
            return sb.ToString();
        }
    }

    public enum MI_LowComparator { GE, GT }
    public enum MI_HighComparator { LE, LT }
    public enum MI_UnitPrefix { NONE, peta, tera, giga, mega, kilo, hecto, deca, deci, centi, milli, micro, nano, pico, femto }
    public enum MI_Units { NONE, Amperes, Celcius, Farads, Henries, Hertz, Ohms, Seconds, Siemens, Volts, VoltAmperes, Watts }
    public enum MI_UnitSuffix { NONE, AC, DC, Peak, PP, RMS }

    public class MP : M, IAssertionCurrent {
        // NOTE: Constructor-less because only instantiated via System.Xml.Serialization.XmlSerializer, thus constructor unnecessary.
        [XmlAttribute(nameof(Path))] public String Path { get; set; }
        [XmlAttribute(nameof(Executable))] public String Executable { get; set; }
        [XmlAttribute(nameof(Parameters))] public String Parameters { get; set; }
        [XmlAttribute(nameof(Expected))] public String Expected { get; set; }

        public String AssertionCurrent() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{TO.DEBUG_ASSERT}{GetType().Name}{TO.BEGIN}");
            sb.Append($"{AssertionM()}{TO.CONTINUE}");
            sb.Append($"{nameof(Path)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Path)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(Executable)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Executable)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(Parameters)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Parameters)).GetValue(this))}{TO.CONTINUE}");
            sb.Append($"{nameof(Expected)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Expected)).GetValue(this))}{TO.END}");
            return sb.ToString();
        }
    }

    public class MT : M, IAssertionCurrent {
        // NOTE: Constructor-less because only instantiated via System.Xml.Serialization.XmlSerializer, thus constructor unnecessary.
        [XmlAttribute(nameof(Text))] public String Text { get; set; }

        public String AssertionCurrent() {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{TO.DEBUG_ASSERT}{GetType().Name}{TO.BEGIN}");
            sb.Append($"{AssertionM()}{TO.CONTINUE}");
            sb.Append($"{nameof(Text)}{TO.CS}{TO.EF(GetType().GetProperty(nameof(Text)).GetValue(this))}{TO.END}");
            return sb.ToString();
        }
    }

    public class Statistics {
        public UInt32 Cancelled = 0;
        public UInt32 EmergencyStopped = 0;
        public UInt32 Errored = 0;
        public UInt32 Failed = 0;
        public UInt32 Ignored = 0;
        public UInt32 Passed = 0;
        private readonly DateTime TestSelected = DateTime.Now;

        public Statistics() { }

        public void Update(EVENTS Event) { 
            switch(Event) {
                case EVENTS.CANCEL:
                    Cancelled++;
                    break;
                case EVENTS.EMERGENCY_STOP:
                    EmergencyStopped++;
                    break;
                case EVENTS.ERROR:
                    Errored++;
                    break;
                case EVENTS.FAIL:
                    Failed++;
                    break;
                case EVENTS.IGNORE:
                    Ignored++;
                    break;
                case EVENTS.PASS:
                    Passed++;
                    break;
                case EVENTS.UNSET:
                    throw new ArgumentException($"Event '{Event}' illegal argument for {System.Reflection.MethodBase.GetCurrentMethod().Name}.");
                default:
                    throw new NotImplementedException($"Event '{Event}' not implemented.");
            }
        }

        public String Time() {
            TimeSpan elapsedTime = DateTime.Now - TestSelected;
            return $"{(elapsedTime.Days != 0 ? elapsedTime.Days.ToString() + ":" : String.Empty)}{elapsedTime.Hours}:{elapsedTime.Minutes:00}";
        }
        public Double PercentCancelled() { return Convert.ToDouble(Cancelled) / Convert.ToDouble(Tested()); }
        public Double PercentEmergencyStopped() { return Convert.ToDouble(EmergencyStopped) / Convert.ToDouble(Tested()); }
        public Double PercentErrored() { return Convert.ToDouble(Errored) / Convert.ToDouble(Tested()); }
        public Double PercentFailed() { return Convert.ToDouble(Failed) / Convert.ToDouble(Tested()); }
        public Double PercentIgnored() { return Convert.ToDouble(Ignored) / Convert.ToDouble(Tested()); }
        public Double PercentPassed() { return Convert.ToDouble(Passed) / Convert.ToDouble(Tested()); }
        public UInt32 Tested() { return Cancelled + EmergencyStopped + Errored + Failed + Ignored + Passed; }
    }
}
