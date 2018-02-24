using System;
using SwitchAppDesign.AniListAPI.v2.Types;
using System.Collections.Generic;
using System.Linq;
using SwitchAppDesign.AniListAPI.v2.Common;
using SwitchAppDesign.AniListAPI.v2.Graph.Arguments;
using SwitchAppDesign.AniListAPI.v2.Graph.Common;
using SwitchAppDesign.AniListAPI.v2.Graph.Types;
using SwitchAppDesign.AniListAPI.v2.Utility;

namespace SwitchAppDesign.AniListAPI.v2.Graph.Fields
{
    /// <summary>
    /// All available character edge query fields.
    /// </summary>
	public class CharacterEdgeQueryFields
	{
		internal CharacterEdgeQueryFields(AniListQueryType queryType)
		{
			InitializeProperties(queryType);
		}

        /// <summary>
        /// <param name="fields">The list of character query fields (found in <see cref="CharacterQueryFields"/>) to be used in the graph query (at least of character query field is required).</param>
        /// </summary>
        public GraphQueryField NodeQueryField(IList<GraphQueryField> fields)
	    {
	        if (fields == null || !fields.Any())
	            throw new GraphQueryFieldInvalidException($"Query field ({nameof(Node)}) requires at least one character query field.");

	        if (fields.Any(x => x.ParentClassType != typeof(CharacterQueryFields)))
	            throw new GraphQueryFieldInvalidException($"The following fields are not valid character query fields {fields.Where(x => x.ParentClassType != typeof(CharacterQueryFields)).Select(x => x.FieldName).Aggregate((x, y) => $"{x}, {y}")}.");

	        return Node.GetGraphFieldAndSetFieldArguments(fields);
	    }

        /// <summary>
        /// The id of the connection
        /// </summary>
        public GraphQueryField IdQueryField()
		{
			return Id;
		}

		/// <summary>
		/// The characters role in the media
		/// </summary>
		public GraphQueryField RoleQueryField()
		{
			return Role;
		}

        /// <summary>
        /// The voice actors of the character.
        /// </summary>
        public GraphQueryField VoiceActorsQueryField(IList<GraphQueryField> fields, IList<GraphQueryArgument<object>> arguments = null)
        {
            if (fields == null || !fields.Any())
                throw new GraphQueryFieldInvalidException($"Query field ({nameof(VoiceActors)}) requires at least one staff query field.");

            if (fields.Any(x => x.ParentClassType != typeof(StaffQueryFields)))
                throw new GraphQueryFieldInvalidException($"The following fields are not valid staff query fields {fields.Where(x => x.ParentClassType != typeof(StaffQueryFields)).Select(x => x.FieldName).Aggregate((x, y) => $"{x}, {y}")}.");

            if (arguments != null)
            {
                if (arguments.Any(x => (Type)x.GetType().GetProperty("ParentClassType").GetValue(x) != typeof(StaffQueryArguments)))
                {
                    throw new GraphQueryArgumentInvalidException($@"The following fields are not valid staff query arguments {
                        arguments
                            .Where(x => (Type)x.GetType().GetProperty("ParentClassType").GetValue(x) != typeof(StaffQueryArguments))
                            .Select(x => x.FieldName)
                            .Aggregate((x, y) => $"{x}, {y}")}.");
                }

                foreach (var argument in arguments)
                    argument.IsValidArgumentType();
            }

            return VoiceActors.GetGraphFieldAndSetFieldArguments(fields, arguments);
        }

        /// <summary>
        /// The media the character is in.
        /// </summary>
        public GraphQueryField MediaQueryField(IList<GraphQueryField> fields)
        {
            if (fields == null || !fields.Any())
                throw new GraphQueryFieldInvalidException($"Query field ({nameof(Media)}) requires at least one media query field.");

            if (fields.Any(x => x.ParentClassType != typeof(MediaQueryFields)))
                throw new GraphQueryFieldInvalidException($"The following fields are not valid media query fields {fields.Where(x => x.ParentClassType != typeof(MediaQueryFields)).Select(x => x.FieldName).Aggregate((x, y) => $"{x}, {y}")}.");

            return Media.GetGraphFieldAndSetFieldArguments(fields);
        }

		/// <summary>
		/// The order the character should be displayed from the users favourites.
		/// </summary>
		public GraphQueryField FavouriteOrderQueryField()
		{
			return FavouriteOrder;
		}

		private GraphQueryField Node { get; set; }
		private GraphQueryField Id { get; set; }
		private GraphQueryField Role { get; set; }
		private GraphQueryField VoiceActors { get; set; }
		private GraphQueryField Media { get; set; }
		private GraphQueryField FavouriteOrder { get; set; }

		private void InitializeProperties(AniListQueryType queryType)
		{
			Node = new GraphQueryField("node", GetType(), queryType, new FieldRules(false, new List<AniListQueryType> { AniListQueryType.Media, AniListQueryType.Staff, AniListQueryType.User }));
			Id = new GraphQueryField("id", GetType(), queryType, new FieldRules(false, new List<AniListQueryType> { AniListQueryType.Media, AniListQueryType.Staff, AniListQueryType.User }));
			Role = new GraphQueryField("role", GetType(), queryType, new FieldRules(false, new List<AniListQueryType> { AniListQueryType.Media, AniListQueryType.Staff, AniListQueryType.User }));
			VoiceActors = new GraphQueryField("voiceActors", GetType(), queryType, new FieldRules(false, new List<AniListQueryType> { AniListQueryType.Media, AniListQueryType.Staff, AniListQueryType.User }));
			Media = new GraphQueryField("media", GetType(), queryType, new FieldRules(false, new List<AniListQueryType> { AniListQueryType.Media, AniListQueryType.Staff, AniListQueryType.User }));
			FavouriteOrder = new GraphQueryField("favouriteOrder", GetType(), queryType, new FieldRules(false, new List<AniListQueryType> { AniListQueryType.Media, AniListQueryType.Staff, AniListQueryType.User }));
		}
	}
}
