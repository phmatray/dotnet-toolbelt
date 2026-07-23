/* Author : 
 * Philippe Matray
 * 
 * Date:
 * 2014-03-13
 */

using System;

namespace SimpleHelpers.Patterns.Repository
{
    public interface IModel
    {
        Guid Id { get; set; }
    }
}