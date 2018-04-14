using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Throwable
{
    /// <summary>
    /// Represents a glass that can be thrown.
    /// </summary>
    public class GlassThrowable : AThrowable
    {
        protected override void OnObjectDestroy()
        {
            throw new NotImplementedException();
        }
    }
}
