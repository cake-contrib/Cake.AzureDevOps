namespace Cake.AzureDevOps
{
    using System.Collections.Generic;
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

            var build = new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory());

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
            this ICakeContext context,
            int buildId)
        {
            context.NotNull(nameof(context));
            buildId.NotNegativeOrZero(nameof(buildId));

            var settings = AzureDevOpsBuildSettings.UsingAzurePipelinesOAuthToken(buildId);

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
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory())
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
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory())
                    .GetChanges();
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
                new AzureDevOpsBuild(context.Log, settings, new BuildClientFactory())
                    .GetTimelineRecords();
        }
    }
}