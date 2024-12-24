using AvalonDock.Layout;
using DryIoc;
using Prism.Regions;
using ProductivityHub.WPF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ProductivityHub.WPF.Core.Adapters
{
    /// <summary>
    /// Adapter for integrating AvalonDock's LayoutDocumentPane with Prism regions.
    /// </summary>
    public class DocumentPaneRegionAdapter : RegionAdapterBase<LayoutDocumentPane>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentPaneRegionAdapter"/> class.
        /// </summary>
        /// <param name="regionBehaviorFactory">The region behavior factory.</param>
        public DocumentPaneRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        /// <summary>
        /// Adapts the region to the LayoutDocumentPane.
        /// </summary>
        /// <param name="region">The region to adapt.</param>
        /// <param name="regionTarget">The LayoutDocumentPane to adapt to.</param>
        protected override void Adapt(IRegion region, LayoutDocumentPane regionTarget)
        {
            if (region == null) throw new ArgumentNullException(nameof(region));
            if (regionTarget == null) throw new ArgumentNullException(nameof(regionTarget));

            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                    {
                        AddDocumentToPane(region, regionTarget, view);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems)
                    {
                        RemoveDocumentFromPane(region, regionTarget, view);
                    }
                }
            };
        }

        /// <summary>
        /// Removes a document from the LayoutDocumentPane.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="regionTarget">The LayoutDocumentPane.</param>
        /// <param name="view">The view to remove.</param>
        private static void RemoveDocumentFromPane(IRegion region, LayoutDocumentPane regionTarget, object view)
        {
            var document = regionTarget.Children.FirstOrDefault(d => d.Content == view);
            if (document != null)
            {
                regionTarget.Children.Remove(document);
                document.Close();
            }
        }

        /// <summary>
        /// Adds a document to the LayoutDocumentPane.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="regionTarget">The LayoutDocumentPane.</param>
        /// <param name="view">The view to add.</param>
        private static void AddDocumentToPane(IRegion region, LayoutDocumentPane regionTarget, object view)
        {
            if (view is not FrameworkElement frameworkElement)
                return;

            var viewModel = frameworkElement.DataContext;
            string documentName = GetDocumentName(viewModel, view);

            var document = new LayoutDocument
            {
                Content = view,
                Title = documentName,
                IsSelected = true
            };

            BindTitleProperty(viewModel, document, nameof(IDocumentLayout.DocumentName));

            document.Closed += (sender, args) =>
            {
                if (region.Views.Contains(view))
                {
                    region.Remove(view);
                }
            };

            regionTarget.Children.Add(document);
        }

        /// <summary>
        /// Gets the document name from the view model or uses the default name.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="view">The view.</param>
        /// <returns>The document name.</returns>
        private static string GetDocumentName(object viewModel, object view)
        {
            if (view is IDocumentLayout docLayout)
                return docLayout.DocumentName;

            if (viewModel == null)
                return view.GetType().Name;

            var documentNameProperty = viewModel.GetType().GetProperty(nameof(IDocumentLayout.DocumentName));
            if (documentNameProperty == null)
                return view.GetType().Name;

            var documentNameValue = documentNameProperty.GetValue(viewModel) as string;
            return !string.IsNullOrEmpty(documentNameValue) ? documentNameValue : view.GetType().Name;
        }

        /// <summary>
        /// Binds the title property of the layout content to the view model property.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="layoutContent">The layout content.</param>
        /// <param name="propertyName">The property name.</param>
        private static void BindTitleProperty(object viewModel, LayoutContent layoutContent, string propertyName)
        {
            if (viewModel == null)
                return;

            var property = viewModel.GetType().GetProperty(propertyName);
            if (property == null)
                return;

            BindingOperations.SetBinding(
                layoutContent,
                LayoutContent.TitleProperty,
                new Binding(propertyName)
                {
                    Source = viewModel,
                    Mode = BindingMode.OneWay
                });
        }

        /// <summary>
        /// Creates a new region.
        /// </summary>
        /// <returns>A new instance of <see cref="AllActiveRegion"/>.</returns>
        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}
