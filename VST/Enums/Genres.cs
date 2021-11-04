using System;

namespace VST
{
    public enum Genres : int
    {
        [StringValue("Rock")]
        Rock,
        [StringValue("Experimental")]
        Experimental,
        [StringValue("Electronic")]
        Electronic,
        [StringValue("Hip-Hop")]
        HipHop,
        [StringValue("Folk")]
        Folk,
        [StringValue("Pop")]
        Pop,
        [StringValue("Instrumental")]
        Instrumental,
        [StringValue("International")]
        International,
        [StringValue("Classical")]
        Classical,
        [StringValue("Jazz")]
        Jazz,
        [StringValue("Old-Time / Historic")]
        OldTime_Historic,
        [StringValue("Spoken")]
        Spoken,
        [StringValue("Country")]
        Country,
        [StringValue("Soul-RnB")]
        Soul_RnB,
        [StringValue("Blues")]
        Blues,
        [StringValue("Easy Listetning")]
        EasyListening,
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
