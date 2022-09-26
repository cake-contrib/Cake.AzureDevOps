namespace Cake.AzureDevOps
{
    using System.Collections.Generic;
    using Cake.AzureDevOps.Boards.WorkItemTracking;
    using Cake.AzureDevOps.Pipelines;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to Azure Pipeline builds.
    /// </content>
    public static partial class AzureDevOpsAliases
    {
        /// <summary>
        /// Gets an Azure Pipelines build using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <example>
        /// <para>Get a build running on Azure DevOps Server:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         42,
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var build =
        ///     AzureDevOpsBuild(
        ///         buildSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the build.
        /// Returns <c>null</c> if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static AzureDevOpsBuild AzureDevOpsBuild(
            this ICakeContext context,
            AzureDevOpsBuildSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var build = new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory(), new TestManagementClientFactory(), new WorkItemTrackingClientFactory());

            if (build.HasBuildLoaded)
            {
                return build;
            }

            return null;
        }

        /// <summary>
        /// Gets the description of the Azure Pipelines build which is running.
        /// Make sure the build has the 'Allow Scripts to access OAuth token' option enabled.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <para>Get current Azure Pipelines build:</para>
        /// <code>
        /// <![CDATA[
        /// var build =
        ///     AzureDevOpsBuildUsingAzurePipelinesOAuthToken();
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the build.
        /// Returns <c>null</c> if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static AzureDevOpsBuild AzureDevOpsBuildUsingAzurePipelinesOAuthToken(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            var settings = AzureDevOpsBuildSettings.UsingAzurePipelinesOAuthToken();

            return AzureDevOpsBuild(context, settings);
        }

        /// <summary>
        /// Gets the description of the Azure Pipelines build which is running.
        /// Make sure the build has the 'Allow Scripts to access OAuth token' option enabled.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="throwExceptionIfBuildCouldNotBeFound">Value indicating whether an exception
        /// should be thrown if build could not be found.</param>
        /// <example>
        /// <para>Get current Azure Pipelines build. Don't throw exception in case the build is not found:</para>
        /// <code>
        /// <![CDATA[
        /// var build =
        ///     AzureDevOpsBuildUsingAzurePipelinesOAuthToken(false);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the build.
        /// Returns <c>null</c> if build could not be found and
        /// <paramref name="throwExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <paramref name="throwExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static AzureDevOpsBuild AzureDevOpsBuildUsingAzurePipelinesOAuthToken(
            this ICakeContext context,
            bool throwExceptionIfBuildCouldNotBeFound)
        {
            context.NotNull(nameof(context));

            var settings = AzureDevOpsBuildSettings.UsingAzurePipelinesOAuthToken();
            settings.ThrowExceptionIfBuildCouldNotBeFound = throwExceptionIfBuildCouldNotBeFound;

            return AzureDevOpsBuild(context, settings);
        }

        /// <summary>
        /// Gets the description of a specific build which is running the access token provided by Azure Pipelines.
        /// Make sure the build has the 'Allow Scripts to access OAuth token' option enabled.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="buildId">ID of the build.</param>
        /// <example>
        /// <para>Get an Azure Pipelines build:</para>
        /// <code>
        /// <![CDATA[
        /// var build =
        ///     AzureDevOpsBuildUsingAzurePipelinesOAuthToken(42);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the build.
        /// Returns <c>null</c> if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static AzureDevOpsBuild AzureDevOpsBuildUsingAzurePipelinesOAuthToken(
            this ICakeContext context,
            int buildId)
        {
            context.NotNull(nameof(context));
            buildId.NotNegativeOrZero(nameof(buildId));

            var settings = AzureDevOpsBuildSettings.UsingAzurePipelinesOAuthToken(buildId);

            return AzureDevOpsBuild(context, settings);
        }

        /// <summary>
        /// Gets the description of a specific build which is running the access token provided by Azure Pipelines.
        /// Make sure the build has the 'Allow Scripts to access OAuth token' option enabled.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="buildId">ID of the build.</param>
        /// <param name="throwExceptionIfBuildCouldNotBeFound">Value indicating whether an exception
        /// should be thrown if build could not be found.</param>
        /// <example>
        /// <para>Get an Azure Pipelines build. Don't throw exception in case the build is not found:</para>
        /// <code>
        /// <![CDATA[
        /// var build =
        ///     AzureDevOpsBuildUsingAzurePipelinesOAuthToken(42, false);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the build.
        /// Returns <c>null</c> if build could not be found and
        /// <paramref name="throwExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <paramref name="throwExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static AzureDevOpsBuild AzureDevOpsBuildUsingAzurePipelinesOAuthToken(
            this ICakeContext context,
            int buildId,
            bool throwExceptionIfBuildCouldNotBeFound)
        {
            context.NotNull(nameof(context));
            buildId.NotNegativeOrZero(nameof(buildId));

            var settings = AzureDevOpsBuildSettings.UsingAzurePipelinesOAuthToken(buildId);
            settings.ThrowExceptionIfBuildCouldNotBeFound = throwExceptionIfBuildCouldNotBeFound;

            return AzureDevOpsBuild(context, settings);
        }

        /// <summary>
        /// Gets Azure Pipelines builds using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <example>
        /// <para>Get builds running on Azure DevOps Server:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildsSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var builds =
        ///     AzureDevOpsBuilds(
        ///         buildSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the builds or an empty list of builds.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static IEnumerable<AzureDevOpsBuild> AzureDevOpsBuilds(
            this ICakeContext context,
            AzureDevOpsBuildsSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return AzureDevOpsBuildsHelper.GetAzureDevOpsBuilds(
                context.Log,
                settings);
        }

        /// <summary>
        /// Returns if the Azure DevOps build is failing.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <example>
        /// <para>Get changes associated with an Azure Pipelines build:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         42,
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var isFailing =
        ///     AzureDevOpsBuildIsFailing(
        ///         buildSettings);
        ///
        /// if (isFailing)
        /// {
        ///     Information("Build is failing");
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The changes associated with the build.
        /// Returns an empty list if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static bool AzureDevOpsBuildIsFailing(
            this ICakeContext context,
            AzureDevOpsBuildSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory(), new TestManagementClientFactory(), new WorkItemTrackingClientFactory())
                    .IsBuildFailing();
        }

        /// <summary>
        /// Gets the changes associated with an Azure Pipelines build.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <example>
        /// <para>Get changes associated with an Azure Pipelines build:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         42,
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var changes =
        ///     AzureDevOpsBuildChanges(
        ///         buildSettings);
        ///
        /// Information("Changes:");
        /// foreach (var change in changes)
        /// {
        ///     Information("  {0}: {1}", change.Id, change.Message);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The changes associated with the build.
        /// Returns an empty list if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static IEnumerable<AzureDevOpsChange> AzureDevOpsBuildChanges(
            this ICakeContext context,
            AzureDevOpsBuildSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory(), new TestManagementClientFactory(), new WorkItemTrackingClientFactory())
                    .GetChanges();
        }

        /// <summary>
        /// Gets the work item ids associated with an Azure Pipelines build.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <example>
        /// <para>Get work item ids associated with an Azure Pipelines build:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         42,
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var workItemIds =
        ///     AzureDevOpsBuildWorkItemIds(
        ///         buildSettings);
        ///
        /// Information("Work item ids:");
        /// foreach (var id in workItemIds)
        /// {
        ///     Information("  {0}", id);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The work item ids associated with the build.
        /// Returns an empty list if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static IEnumerable<int> AzureDevOpsBuildWorkItemIds(
            this ICakeContext context,
            AzureDevOpsBuildSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory(), new TestManagementClientFactory(), new WorkItemTrackingClientFactory())
                    .GetWorkItemIds();
        }

        /// <summary>
        /// Gets the work items associated with an Azure Pipelines build.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <example>
        /// <para>Get work items associated with an Azure Pipelines build:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         42,
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var workItems =
        ///     AzureDevOpsBuildWorkItems(
        ///         buildSettings);
        ///
        /// Information("Work item:");
        /// foreach (var workItem in workItems)
        /// {
        ///     Information("  {0}: {1}", workItem.Id, workItem.Title);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The work item ids associated with the build.
        /// Returns an empty list if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static IEnumerable<AzureDevOpsWorkItem> AzureDevOpsBuildWorkItems(
            this ICakeContext context,
            AzureDevOpsBuildSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory(), new TestManagementClientFactory(), new WorkItemTrackingClientFactory())
                    .GetWorkItems();
        }

        /// <summary>
        /// Gets the timeline entries for an Azure Pipelines build.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <example>
        /// <para>Get timeline entries for an Azure Pipelines build:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         42,
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var timelineRecords =
        ///     AzureDevOpsBuildTimelineRecords(
        ///         buildSettings);
        ///
        /// Information("Timeline:");
        /// foreach (var timelineRecord in timelineRecords)
        /// {
        ///     Information("  {0}: {1}", timelineRecord.Name, timelineRecord.Result);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The timeline entries for the build.
        /// Returns an empty list if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static IEnumerable<AzureDevOpsTimelineRecord> AzureDevOpsBuildTimelineRecords(
            this ICakeContext context,
            AzureDevOpsBuildSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory(), new TestManagementClientFactory(), new WorkItemTrackingClientFactory())
                    .GetTimelineRecords();
        }

        /// <summary>
        /// Gets the build artifacts for an Azure Pipelines build.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <example>
        /// <para>Get build artifacts for an Azure Pipelines build:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         42,
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var buildArtifacts =
        ///     AzureDevOpsBuildArtifacts(
        ///         buildSettings);
        ///
        /// Information("Build artifacts:");
        /// foreach (var buildArtifact in buildArtifacts)
        /// {
        ///     Information("  {0}: {1}", buildArtifact.Name, buildArtifact.Resource.Url);
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The artifacts of the build.
        /// Returns an empty list if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static IEnumerable<AzureDevOpsBuildArtifact> AzureDevOpsBuildArtifacts(
            this ICakeContext context,
            AzureDevOpsBuildSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory(), new TestManagementClientFactory(), new WorkItemTrackingClientFactory())
                    .GetArtifacts();
        }

        /// <summary>
        /// Gets the test runs for an Azure Pipelines build.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <example>
        /// <para>Get test runs for an Azure Pipelines build:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         42,
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var testRuns =
        ///     AzureDevOpsBuildTestRuns(
        ///         buildSettings);
        ///
        /// Information("Test runs:");
        /// foreach (var testRun in testRuns)
        /// {
        ///     Information("  {0}: {1}", testRun.RunId, testRun.TestResults.Count());
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Test runs for the build.
        /// Returns an empty list if build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsBuildNotFoundException">If build could not be found and
        /// <see cref="AzureDevOpsBuildSettings.ThrowExceptionIfBuildCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static IEnumerable<AzureDevOpsTestRun> AzureDevOpsBuildTestRuns(
            this ICakeContext context,
            AzureDevOpsBuildSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory(), new TestManagementClientFactory(), new WorkItemTrackingClientFactory())
                    .GetTestRuns();
        }

        /// <summary>
        /// Gets Azure Pipelines build definitions for the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the build definitions.</param>
        /// <example>
        /// <para>Get build definitions running on Azure DevOps Server:</para>
        /// <code>
        /// <![CDATA[
        /// var buildSettings =
        ///     new AzureDevOpsBuildsSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var buildDefinitions =
        ///     AzureDevOpsBuildDefinitions(
        ///         buildSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The build definitions or an empty list of build definitions.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Pipelines")]
        public static IEnumerable<AzureDevOpsBuildDefinition> AzureDevOpsBuildDefinitions(
            this ICakeContext context,
            AzureDevOpsBuildsSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return AzureDevOpsBuildsDefinitionHelper.GetAzureDevOpsBuildDefinitions(
                context.Log,
                settings);
        }
    }
}
