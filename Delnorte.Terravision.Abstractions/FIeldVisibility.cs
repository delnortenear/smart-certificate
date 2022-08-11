using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delnorte.Terravision.Abstractions
{
    public readonly struct FieldVisibility
    {
        /// <summary>
        /// Stores configuration name 
        /// </summary>
        [JsonIgnore]
        public readonly string Configuration;

        /// <summary>
        /// Stores path to field
        /// </summary>
        public readonly string Path;

        /// <summary>
        /// Stores Field name
        /// </summary>
        public readonly string Name;

        public FieldVisibility(string config, string name, string path = "/")
        {
            this.Configuration = config;
            this.Path = path;   
            this.Name = name;
        }
    }
}
