using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.Design.WebControls;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace MAT.Web.UI
{
    /// <summary>
    /// A designer class for a strongly typed repeater <c>ButacaRepeater</c>
    /// </summary>
	public class ButacaRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:ButacaRepeaterDesigner"/> class.
        /// </summary>
		public ButacaRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is ButacaRepeater))
			{ 
				throw new ArgumentException("Component is not a ButacaRepeater."); 
			} 
			base.Initialize(component); 
			base.SetViewFlags(ViewFlags.TemplateEditing, true); 
		}


		/// <summary>
		/// Generate HTML for the designer
		/// </summary>
		/// <returns>a string of design time HTML</returns>
		public override string GetDesignTimeHtml()
		{

			// Get the instance this designer applies to
			//
			ButacaRepeater z = (ButacaRepeater)Component;
			z.DataBind();

			return base.GetDesignTimeHtml();

			//return z.RenderAtDesignTime();

			//	ControlCollection c = z.Controls;
			//Totem z = (Totem) Component;
			//Totem z = (Totem) Component;
			//return ("<div style='border: 1px gray dotted; background-color: lightgray'><b>TagStat :</b> zae |  qsdds</div>");

		}
	}

    /// <summary>
    /// A strongly typed repeater control for the <see cref="ButacaRepeater"/> Type.
    /// </summary>
	[Designer(typeof(ButacaRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:ButacaRepeater runat=\"server\"></{0}:ButacaRepeater>")]
	public class ButacaRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:ButacaRepeater"/> class.
        /// </summary>
		public ButacaRepeater()
		{
		}

		/// <summary>
        /// Gets a <see cref="T:System.Web.UI.ControlCollection"></see> object that represents the child controls for a specified server control in the UI hierarchy.
        /// </summary>
        /// <value></value>
        /// <returns>The collection of child controls for the specified server control.</returns>
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		private ITemplate m_headerTemplate;
		/// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The header template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(ButacaItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate HeaderTemplate
		{
			get { return m_headerTemplate; }
			set { m_headerTemplate = value; }
		}

		private ITemplate m_itemTemplate;
		/// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(ButacaItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate ItemTemplate
		{
			get { return m_itemTemplate; }
			set { m_itemTemplate = value; }
		}

		private ITemplate m_seperatorTemplate;
        /// <summary>
        /// Gets or sets the Seperator Template
        /// </summary>
        [Browsable(false)]
        [TemplateContainer(typeof(ButacaItem))]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public ITemplate SeperatorTemplate
        {
            get { return m_seperatorTemplate; }
            set { m_seperatorTemplate = value; }
        }
			
		private ITemplate m_altenateItemTemplate;
        /// <summary>
        /// Gets or sets the alternating item template.
        /// </summary>
        /// <value>The alternating item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(ButacaItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate AlternatingItemTemplate
		{
			get { return m_altenateItemTemplate; }
			set { m_altenateItemTemplate = value; }
		}

		private ITemplate m_footerTemplate;
        /// <summary>
        /// Gets or sets the footer template.
        /// </summary>
        /// <value>The footer template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(ButacaItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate FooterTemplate
		{
			get { return m_footerTemplate; }
			set { m_footerTemplate = value; }
		}

//      /// <summary>
//      /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
//      /// </summary>
//		protected override void CreateChildControls()
//      {
//         if (ChildControlsCreated)
//         {
//            return;
//         }

//         Controls.Clear();

//         //Instantiate the Header template (if exists)
//         if (m_headerTemplate != null)
//         {
//            Control headerItem = new Control();
//            m_headerTemplate.InstantiateIn(headerItem);
//            Controls.Add(headerItem);
//         }

//         //Instantiate the Footer template (if exists)
//         if (m_footerTemplate != null)
//         {
//            Control footerItem = new Control();
//            m_footerTemplate.InstantiateIn(footerItem);
//            Controls.Add(footerItem);
//         }
//
//         ChildControlsCreated = true;
//      }
	
		/// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            
        }

        /// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
                
        }		
		
		/// <summary>
      	/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
      	/// </summary>
		protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
      	{
         int pos = 0;

         if (dataBinding)
         {
            //Instantiate the Header template (if exists)
            if (m_headerTemplate != null)
            {
                Control headerItem = new Control();
                m_headerTemplate.InstantiateIn(headerItem);
                Controls.Add(headerItem);
            }
			if (dataSource != null)
			{
				foreach (object o in dataSource)
				{
						MAT.Entities.Butaca entity = o as MAT.Entities.Butaca;
						ButacaItem container = new ButacaItem(entity);
	
						if (m_itemTemplate != null && (pos % 2) == 0)
						{
							m_itemTemplate.InstantiateIn(container);
							
							if (m_seperatorTemplate != null)
							{
								m_seperatorTemplate.InstantiateIn(container);
							}
						}
						else
						{
							if (m_altenateItemTemplate != null)
							{
								m_altenateItemTemplate.InstantiateIn(container);
								
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}
								
							}
							else if (m_itemTemplate != null)
							{
								m_itemTemplate.InstantiateIn(container);
								
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}
							}
							else
							{
								// no template !!!
							}
						}
						Controls.Add(container);
						
						container.DataBind();
						
						pos++;
				}
			}
            //Instantiate the Footer template (if exists)
            if (m_footerTemplate != null)
            {
                Control footerItem = new Control();
                m_footerTemplate.InstantiateIn(footerItem);
                Controls.Add(footerItem);
            }

		}
			
			return pos;
		}

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
		protected override void OnPreRender(EventArgs e)
		{
			base.DataBind();
		}

		#region Design time
        /// <summary>
        /// Renders at design time.
        /// </summary>
        /// <returns>a  string of the Designed HTML</returns>
		internal string RenderAtDesignTime()
		{			
			return "Designer currently not implemented"; 
		}

		#endregion
	}

    /// <summary>
    /// A wrapper type for the entity
    /// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class ButacaItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MAT.Entities.Butaca _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ButacaItem"/> class.
        /// </summary>
		public ButacaItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ButacaItem"/> class.
        /// </summary>
		public ButacaItem(MAT.Entities.Butaca entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the ButacaId
        /// </summary>
        /// <value>The ButacaId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid ButacaId
		{
			get { return _entity.ButacaId; }
		}
        /// <summary>
        /// Gets the NroButaca
        /// </summary>
        /// <value>The NroButaca.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? NroButaca
		{
			get { return _entity.NroButaca; }
		}
        /// <summary>
        /// Gets the Piso
        /// </summary>
        /// <value>The Piso.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Piso
		{
			get { return _entity.Piso; }
		}
        /// <summary>
        /// Gets the Ubicacion
        /// </summary>
        /// <value>The Ubicacion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Ubicacion
		{
			get { return _entity.Ubicacion; }
		}
        /// <summary>
        /// Gets the Tipo
        /// </summary>
        /// <value>The Tipo.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Tipo
		{
			get { return _entity.Tipo; }
		}
        /// <summary>
        /// Gets the TransporteId
        /// </summary>
        /// <value>The TransporteId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid? TransporteId
		{
			get { return _entity.TransporteId; }
		}
        /// <summary>
        /// Gets the Fila
        /// </summary>
        /// <value>The Fila.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Fila
		{
			get { return _entity.Fila; }
		}
        /// <summary>
        /// Gets the Posicion
        /// </summary>
        /// <value>The Posicion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Posicion
		{
			get { return _entity.Posicion; }
		}
        /// <summary>
        /// Gets the CodigoButaca
        /// </summary>
        /// <value>The CodigoButaca.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String CodigoButaca
		{
			get { return _entity.CodigoButaca; }
		}

        /// <summary>
        /// Gets a <see cref="T:MAT.Entities.Butaca"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MAT.Entities.Butaca Entity
        {
            get { return _entity; }
        }
	}
}
