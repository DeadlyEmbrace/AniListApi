using SwitchAppDesign.AniListAPI.v2.Types;
using System.Collections.Generic;
using System.Linq;
using SwitchAppDesign.AniListAPI.v2.Common;
using SwitchAppDesign.AniListAPI.v2.Graph.Common;
using SwitchAppDesign.AniListAPI.v2.Graph.Types;

namespace SwitchAppDesign.AniListAPI.v2.Graph.Fields
{
    /// <summary>
    /// All available studio connection query fields.
    /// </summary>
	public class StudioConnectionQueryFields
	{
		internal StudioConnectionQueryFields(AniListQueryType queryType)
		{
			InitializeProperties(queryType);
		}

        /// <summary>
        /// <param name="fields">The list of studio edge query fields (found in <see cref="StudioEdgeQueryFields"/>) to be used in the graph query (at least of studio edge query field is required).</param>
        /// </summary>
        public GraphQueryField EdgesQueryField(IList<GraphQueryField> fields)
		{
		    if (fields == null || !fields.Any())
		        throw new GraphQueryFieldInvalidException($"Query field ({nameof(Edges)}) requires at least one studio edge query field.");

		    if (fields.Any(x => x.ParentClassType != typeof(StudioEdgeQueryFields)))
		        throw new GraphQueryFieldInvalidException($"The following fields are not valid studio edge query fields {fields.Where(x => x.ParentClassType != typeof(StudioEdgeQueryFields)).Select(x => x.FieldName).Aggregate((x, y) => $"{x}, {y}")}.");

		    return Edges.GetGraphFieldAndSetFieldArguments(fields);
        }

        /// <summary>
        /// <param name="fields">The list of studio query fields (found in <see cref="StudioQueryFields"/>) to be used in the graph query (at least of studio query field is required).</param>
        /// </summary>
        public GraphQueryField NodesQueryField(IList<GraphQueryField> fields)
		{
		    if (fields == null || !fields.Any())
		        throw new GraphQueryFieldInvalidException($"Query field ({nameof(Nodes)}) requires at least one studio query field.");

		    if (fields.Any(x => x.ParentClassType != typeof(StudioQueryFields)))
		        throw new GraphQueryFieldInvalidException($"The following fields are not valid studio query fields {fields.Where(x => x.ParentClassType != typeof(StudioQueryFields)).Select(x => x.FieldName).Aggregate((x, y) => $"{x}, {y}")}.");

		    return Nodes.GetGraphFieldAndSetFieldArguments(fields);
        }

	    /// <summary>
	    /// The pagination information
	    /// </summary>
	    public GraphQueryField PageInfoQueryField(IList<GraphQueryField> fields)
	    {
	        if (fields == null || !fields.Any())
	            throw new GraphQueryFieldInvalidException($"Query field ({nameof(PageInfo)}) requires at least one page info query field.");

	        if (fields.Any(x => x.ParentClassType != typeof(PageInfoQueryFields)))
	            throw new GraphQueryFieldInvalidException($"The following fields are not valid page info query fields {fields.Where(x => x.ParentClassType != typeof(PageInfoQueryFields)).Select(x => x.FieldName).Aggregate((x, y) => $"{x}, {y}")}.");

	        return PageInfo.GetGraphFieldAndSetFieldArguments(fields);
	    }

        private GraphQueryField Edges { get; set; }
		private GraphQueryField Nodes { get; set; }
		private GraphQueryField PageInfo { get; set; }

		private void InitializeProperties(AniListQueryType queryType)
		{
			Edges = new GraphQueryField("edges", GetType(), queryType, new FieldRules(false, new List<AniListQueryType> { AniListQueryType.Studio }));
			Nodes = new GraphQueryField("nodes", GetType(), queryType, new FieldRules(false, new List<AniListQueryType> { AniListQueryType.Studio }));
			PageInfo = new GraphQueryField("pageInfo", GetType(), queryType, new FieldRules(false, new List<AniListQueryType> { AniListQueryType.Studio }));
		}
	}
}
