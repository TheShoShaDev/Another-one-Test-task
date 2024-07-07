using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
	public interface IDamageble
	{
		public event Action<float> GotDamage;
		void GetDamage(float value);
	}
}
