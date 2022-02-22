using System;

namespace VST
{
    public enum Genres : int
    {
        [StringValue("Hip-Hop")]
        HipHop,
        [StringValue("Pop")]
        Pop,
        [StringValue("Folk")]
        Folk,
        [StringValue("Experimental")]
        Experimental,
        [StringValue("Rock")]
        Rock,
        [StringValue("International")]
        International,
        [StringValue("Electronic")]
        Electronic,
        [StringValue("Instrumental")]
        Instrumental,
        NONE
    }

    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    public class StringValueAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            StringValue = value;
        }

        #endregion

    }
}
