using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstaCrafter.Core
{
    /// <summary>
    /// Helper class that provides basic name and address information for Actors.
    /// 
    /// That way if we need to change the name of an actor, we only need to do it in one place.
    /// </summary>
    public static class ActorNames
    {
        public static readonly string ActorSystemName = "InstaCrafterSystem";

		public static readonly ActorData CrafterJobCoordinator = new ActorData("CrafterJobCoordinator");

		public static readonly ActorData CraftUserMediaActor = new ActorData("CraftUserMediaActor");

		public static readonly ActorData CraftUsersActor = new ActorData("CraftUsersActor");
    }

    /// <summary>
    /// Meta-data class for working with high-level Actor names and paths
    /// </summary>
    public class ActorData
    {
        public ActorData(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}