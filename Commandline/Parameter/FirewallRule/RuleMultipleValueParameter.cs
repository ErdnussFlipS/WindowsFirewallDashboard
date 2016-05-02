﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter.Value;

namespace WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule
{
	public abstract class RuleMultipleValueParameter<ParameterType, ParameterValueType> : NetshExtendedMultipleValueParameter<ParameterType, ParameterValueType>
		where ParameterType : RuleMultipleValueParameter<ParameterType, ParameterValueType>, new()
		where ParameterValueType : NetshExtendedParameterValue<ParameterValueType>, new()
	{
		internal RuleMultipleValueParameter(string name, params ParameterValueType[] value) : base(name, value) { }
	}
}
