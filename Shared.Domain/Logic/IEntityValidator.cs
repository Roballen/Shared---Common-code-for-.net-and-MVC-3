﻿#region

using System.Linq;

#endregion

namespace Shared.Domain.Logic
{
    public interface IEntityValidator
    {
        bool Validate(EntityBase entity);
    }

    public class ExceptionThrowerEntityValidator : IEntityValidator
    {
        #region IEntityValidator Members

        public bool Validate(EntityBase entity)
        {
            if (entity.GetBrokenRules().Any())
            {
                throw new BusinessRuleException("", entity.GetBrokenRules());
            }
            return true;
        }

        #endregion
    }
}