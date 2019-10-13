
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"IAzureDevOpsCredentials",
            content:"IAzureDevOpsCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.Authentication/IAzureDevOpsCredentials',
            title:"IAzureDevOpsCredentials",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"AzureDevOpsPullRequestStatusState",
            content:"AzureDevOpsPullRequestStatusState",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsPullRequestStatusState',
            title:"AzureDevOpsPullRequestStatusState",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"AzureDevOpsBranchNotFoundException",
            content:"AzureDevOpsBranchNotFoundException",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsBranchNotFoundException',
            title:"AzureDevOpsBranchNotFoundException",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"AzureDevOpsAliases",
            content:"AzureDevOpsAliases",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps/AzureDevOpsAliases',
            title:"AzureDevOpsAliases",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"AzureDevOpsCommentType",
            content:"AzureDevOpsCommentType",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest.CommentThread/AzureDevOpsCommentType',
            title:"AzureDevOpsCommentType",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"AzureDevOpsCommentThreadStatus",
            content:"AzureDevOpsCommentThreadStatus",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest.CommentThread/AzureDevOpsCommentThreadStatus',
            title:"AzureDevOpsCommentThreadStatus",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"AzureDevOpsComment",
            content:"AzureDevOpsComment",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest.CommentThread/AzureDevOpsComment',
            title:"AzureDevOpsComment",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"AzureDevOpsPullRequestState",
            content:"AzureDevOpsPullRequestState",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsPullRequestState',
            title:"AzureDevOpsPullRequestState",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"AzureDevOpsException",
            content:"AzureDevOpsException",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps/AzureDevOpsException',
            title:"AzureDevOpsException",
            description:""
        }
    );
    a(
        {
            id:9,
            title:"BaseAzureDevOpsPullRequestSettings",
            content:"BaseAzureDevOpsPullRequestSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/BaseAzureDevOpsPullRequestSettings',
            title:"BaseAzureDevOpsPullRequestSettings",
            description:""
        }
    );
    a(
        {
            id:10,
            title:"AzureDevOpsCreatePullRequestSettings",
            content:"AzureDevOpsCreatePullRequestSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsCreatePullRequestSettings',
            title:"AzureDevOpsCreatePullRequestSettings",
            description:""
        }
    );
    a(
        {
            id:11,
            title:"AzureDevOpsPullRequestIterationChange",
            content:"AzureDevOpsPullRequestIterationChange",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsPullRequestIterationChange',
            title:"AzureDevOpsPullRequestIterationChange",
            description:""
        }
    );
    a(
        {
            id:12,
            title:"AzureDevOpsAadCredentials",
            content:"AzureDevOpsAadCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.Authentication/AzureDevOpsAadCredentials',
            title:"AzureDevOpsAadCredentials",
            description:""
        }
    );
    a(
        {
            id:13,
            title:"AzureDevOpsPullRequestCommentThread",
            content:"AzureDevOpsPullRequestCommentThread",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest.CommentThread/AzureDevOpsPullRequestCommentThread',
            title:"AzureDevOpsPullRequestCommentThread",
            description:""
        }
    );
    a(
        {
            id:14,
            title:"AzureDevOpsPullRequestSettings",
            content:"AzureDevOpsPullRequestSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsPullRequestSettings',
            title:"AzureDevOpsPullRequestSettings",
            description:""
        }
    );
    a(
        {
            id:15,
            title:"AzureDevOpsPullRequestVote",
            content:"AzureDevOpsPullRequestVote",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsPullRequestVote',
            title:"AzureDevOpsPullRequestVote",
            description:""
        }
    );
    a(
        {
            id:16,
            title:"AzureDevOpsPullRequestStatus",
            content:"AzureDevOpsPullRequestStatus",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsPullRequestStatus',
            title:"AzureDevOpsPullRequestStatus",
            description:""
        }
    );
    a(
        {
            id:17,
            title:"AzureDevOpsNtlmCredentials",
            content:"AzureDevOpsNtlmCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.Authentication/AzureDevOpsNtlmCredentials',
            title:"AzureDevOpsNtlmCredentials",
            description:""
        }
    );
    a(
        {
            id:18,
            title:"AzureDevOpsPullRequestNotFoundException",
            content:"AzureDevOpsPullRequestNotFoundException",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsPullRequestNotFoundException',
            title:"AzureDevOpsPullRequestNotFoundException",
            description:""
        }
    );
    a(
        {
            id:19,
            title:"AzureDevOpsPullRequest",
            content:"AzureDevOpsPullRequest",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.PullRequest/AzureDevOpsPullRequest',
            title:"AzureDevOpsPullRequest",
            description:""
        }
    );
    a(
        {
            id:20,
            title:"AzureDevOpsOAuthCredentials",
            content:"AzureDevOpsOAuthCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.Authentication/AzureDevOpsOAuthCredentials',
            title:"AzureDevOpsOAuthCredentials",
            description:""
        }
    );
    a(
        {
            id:21,
            title:"AzureDevOpsBasicCredentials",
            content:"AzureDevOpsBasicCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.AzureDevOps/api/Cake.AzureDevOps.Authentication/AzureDevOpsBasicCredentials',
            title:"AzureDevOpsBasicCredentials",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
