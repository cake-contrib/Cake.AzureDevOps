
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"TfsException",
        content:"TfsException",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"ITfsCredentials",
        content:"ITfsCredentials",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"TfsBasicCredentials",
        content:"TfsBasicCredentials",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"TfsPullRequest",
        content:"TfsPullRequest",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"TfsPullRequestVote",
        content:"TfsPullRequestVote",
        description:'',
        tags:''
    });

    a({
        id:5,
        title:"TfsOAuthCredentials",
        content:"TfsOAuthCredentials",
        description:'',
        tags:''
    });

    a({
        id:6,
        title:"TfsPullRequestNotFoundException",
        content:"TfsPullRequestNotFoundException",
        description:'',
        tags:''
    });

    a({
        id:7,
        title:"TfsNtlmCredentials",
        content:"TfsNtlmCredentials",
        description:'',
        tags:''
    });

    a({
        id:8,
        title:"TfsAadCredentials",
        content:"TfsAadCredentials",
        description:'',
        tags:''
    });

    a({
        id:9,
        title:"TfsAliases",
        content:"TfsAliases",
        description:'',
        tags:''
    });

    a({
        id:10,
        title:"TfsPullRequestSettings",
        content:"TfsPullRequestSettings",
        description:'',
        tags:''
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs/TfsException',
        title:"TfsException",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs.Authentication/ITfsCredentials',
        title:"ITfsCredentials",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs.Authentication/TfsBasicCredentials',
        title:"TfsBasicCredentials",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequest',
        title:"TfsPullRequest",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequestVote',
        title:"TfsPullRequestVote",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs.Authentication/TfsOAuthCredentials',
        title:"TfsOAuthCredentials",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequestNotFoundException',
        title:"TfsPullRequestNotFoundException",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs.Authentication/TfsNtlmCredentials',
        title:"TfsNtlmCredentials",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs.Authentication/TfsAadCredentials',
        title:"TfsAadCredentials",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs/TfsAliases',
        title:"TfsAliases",
        description:""
    });

    y({
        url:'/Cake.Tfs/api/Cake.Tfs.PullRequest/TfsPullRequestSettings',
        title:"TfsPullRequestSettings",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
