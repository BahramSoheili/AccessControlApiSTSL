﻿using CommandManagement.Domains.AccessControl.ValueObjects;
using System;

namespace CommandManagement.Domains.AccessControl.Views
{
    public class DeviceTypeView
    {
        public Guid Id { get; set; }
        public DeviceTypeInfo Data { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
