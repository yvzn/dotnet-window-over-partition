
using System;

namespace MaxOverPartition.Model
{
    public class Contract: ICloneable
    {
        public string Reference { get; init; }
        public int Version { get; set; }

        public Contract(string reference, int version = 1)
        {
            Reference = reference;
            Version = version;
        }

        public string? DummyPropertyA { get; set; }
        public string? DummyPropertyB { get; set; }
        public string? DummyPropertyC { get; set; }
        public string? DummyPropertyD { get; set; }
        public string? DummyPropertyE { get; set; }
        public string? DummyPropertyF { get; set; }
        public string? DummyPropertyG { get; set; }
        public string? DummyPropertyH { get; set; }
        public string? DummyPropertyI { get; set; }
        public string? DummyPropertyJ { get; set; }
        public string? DummyPropertyK { get; set; }
        public string? DummyPropertyL { get; set; }
        public string? DummyPropertyM { get; set; }
        public string? DummyPropertyN { get; set; }
        public string? DummyPropertyO { get; set; }
        public string? DummyPropertyP { get; set; }
        public string? DummyPropertyQ { get; set; }
        public string? DummyPropertyR { get; set; }
        public string? DummyPropertyS { get; set; }
        public string? DummyPropertyT { get; set; }
        public string? DummyPropertyU { get; set; }
        public string? DummyPropertyV { get; set; }
        public string? DummyPropertyW { get; set; }
        public string? DummyPropertyX { get; set; }
        public string? DummyPropertyY { get; set; }
        public string? DummyPropertyZ { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
