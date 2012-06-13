using System.Linq;
using System.Collections.Generic;

namespace Spooker.Web.Infrastructure
{
    public class ReflectiveToStringBuilder
    {
        private readonly object _target;
        private readonly ISet<string> _propertyExcludes = new HashSet<string>();

        public ReflectiveToStringBuilder(object target)
        {
            _target = target;
        }

        public ReflectiveToStringBuilder Exclude(string propertyName)
        {
            _propertyExcludes.Add(propertyName);
            return this;
        }

        public override string ToString()
        {
            if (_target == null)
                return "null";

            var targetType = _target.GetType();
            var properties = targetType.GetProperties();
            var fieldNamesAndValues = properties
                .Where(p => !_propertyExcludes.Contains(p.Name))
                .Select(p => new { PropName = p.Name, PropValue = p.GetValue(_target, null) });
            return _target.GetType().Name + "[" + string.Join(", ", fieldNamesAndValues.Select(fav => fav.PropName + "='" + fav.PropValue + "'")) + "]";
        }
    }
}
