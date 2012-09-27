/* Yet Another Forum.net
 * Copyright (C) 2006-2012 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */
namespace YAF.Types.Extensions
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data;

    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    ///     The db function extensions.
    /// </summary>
    public static class DbFunctionExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The begin unit of work.
        /// </summary>
        /// <param name="dbFunction">
        /// The db function.
        /// </param>
        /// <param name="createUnitOfWork">
        /// The create unit of work.
        /// </param>
        /// <param name="isolationLevel">
        /// The isolation level.
        /// </param>
        /// <returns>
        /// The <see cref="IDbUnitOfWork"/>.
        /// </returns>
        public static IDbUnitOfWork BeginUnitOfWork(
            [NotNull] this IDbFunction dbFunction, 
            [NotNull] ICreateUnitOfWork createUnitOfWork, 
            IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            CodeContracts.ArgumentNotNull(dbFunction, "dbFunction");
            CodeContracts.ArgumentNotNull(createUnitOfWork, "createUnitOfWork");

            dbFunction.UnitOfWork = createUnitOfWork.BeginTransaction(isolationLevel);
            return dbFunction.UnitOfWork;
        }

        /// <summary>
        /// The get data typed.
        /// </summary>
        /// <param name="dbFunction">
        /// The db function. 
        /// </param>
        /// <param name="function">
        /// The function. 
        /// </param>
        /// <param name="comparer">
        /// The comparer. 
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        [CanBeNull]
        public static IEnumerable<T> GetDataTyped<T>(
            [NotNull] this IDbFunction dbFunction, 
            [NotNull] Func<object, object> function, 
            [CanBeNull] IEqualityComparer<string> comparer = null) where T : IDataLoadable, new()
        {
            CodeContracts.ArgumentNotNull(dbFunction, "dbFunction");
            CodeContracts.ArgumentNotNull(function, "function");

            return dbFunction.GetData(function).Typed<T>(comparer);
        }

        /// <summary>
        /// The get scalar as.
        /// </summary>
        /// <param name="dbFunction">
        /// The db function. 
        /// </param>
        /// <param name="function">
        /// The function. 
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        [CanBeNull]
        public static T GetScalar<T>([NotNull] this IDbFunction dbFunction, [NotNull] Func<object, object> function)
        {
            CodeContracts.ArgumentNotNull(dbFunction, "dbFunction");
            CodeContracts.ArgumentNotNull(function, "function");

            return ((object)function(dbFunction.Scalar)).ToType<T>();
        }

        #endregion
    }
}