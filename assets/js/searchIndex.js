
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
            title:"TfsOAuthCredentials",
            content:"TfsOAuthCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.Authentication/TfsOAuthCredentials',
            title:"TfsOAuthCredentials",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"TfsPullRequestVote",
            content:"TfsPullRequestVote",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequestVote',
            title:"TfsPullRequestVote",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"TfsPullRequestStatusState",
            content:"TfsPullRequestStatusState",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequestStatusState',
            title:"TfsPullRequestStatusState",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"ITfsCredentials",
            content:"ITfsCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.Authentication/ITfsCredentials',
            title:"ITfsCredentials",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"TfsAadCredentials",
            content:"TfsAadCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.Authentication/TfsAadCredentials',
            title:"TfsAadCredentials",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"IGitClientFactory",
            content:"IGitClientFactory",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs/IGitClientFactory',
            title:"IGitClientFactory",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"TfsPullRequest",
            content:"TfsPullRequest",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequest',
            title:"TfsPullRequest",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"TfsPullRequestNotFoundException",
            content:"TfsPullRequestNotFoundException",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequestNotFoundException',
            title:"TfsPullRequestNotFoundException",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"TfsPullRequestStatus",
            content:"TfsPullRequestStatus",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequestStatus',
            title:"TfsPullRequestStatus",
            description:""
        }
    );
    a(
        {
            id:9,
            title:"TfsNtlmCredentials",
            content:"TfsNtlmCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.Authentication/TfsNtlmCredentials',
            title:"TfsNtlmCredentials",
            description:""
        }
    );
    a(
        {
            id:10,
            title:"TfsAliases",
            content:"TfsAliases",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs/TfsAliases',
            title:"TfsAliases",
            description:""
        }
    );
    a(
        {
            id:11,
            title:"TfsPullRequestSettings",
            content:"TfsPullRequestSettings",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequestSettings',
            title:"TfsPullRequestSettings",
            description:""
        }
    );
    a(
        {
            id:12,
            title:"TfsException",
            content:"TfsException",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs/TfsException',
            title:"TfsException",
            description:""
        }
    );
    a(
        {
            id:13,
            title:"TfsBasicCredentials",
            content:"TfsBasicCredentials",
            description:'',
            tags:''
        },
        {
            url:'/Cake.Tfs/api/Cake.Tfs.Authentication/TfsBasicCredentials',
            title:"TfsBasicCredentials",
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
