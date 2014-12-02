using System;

namespace Nest
{
    public class Device
    {
        public string locale { get; set; }
        public string temperature_scale { get; set; }
        public bool is_using_emergency_heat { get; set; }
        public bool has_fan { get; set; }
        public string software_version { get; set; }
        public bool has_leaf { get; set; }
        public string device_id { get; set; }
        public string name { get; set; }
        public string structure_id { get; set; }
        public string hvac_mode { get; set; }
        public bool can_heat { get; set; }
        public bool can_cool { get; set; }
        public float target_temperature_c { get; set; }
        public float target_temperature_f { get; set; }
        public float target_temperature_high_c { get; set; }
        public float target_temperature_high_f { get; set; }
        public float target_temperature_low_c { get; set; }
        public float target_temperature_low_f { get; set; }
        public float ambient_temperature_c { get; set; }
        public float ambient_temperature_f { get; set; }
        public float away_temperature_high_c { get; set; }
        public float away_temperature_high_f { get; set; }
        public float away_temperature_low_c { get; set; }
        public float away_temperature_low_f { get; set; }
        public string name_long { get; set; }
        public bool fan_timer_active { get; set; }
        public bool is_online { get; set; }
        public DateTime last_connection { get; set; }
    }
}
