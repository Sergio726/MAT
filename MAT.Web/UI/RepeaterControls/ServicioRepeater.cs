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
    /// A designer class for a strongly typed repeater <c>ServicioRepeater</c>
    /// </summary>
	public class ServicioRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:ServicioRepeaterDesigner"/> class.
        /// </summary>
		public ServicioRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is ServicioRepeater))
			{ 
				throw new ArgumentException("Component is not a ServicioRepeater."); 
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
			ServicioRepeater z = (ServicioRepeater)Component;
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
    /// A strongly typed repeater control for the <see cref="ServicioRepeater"/> Type.
    /// </summary>
	[Designer(typeof(ServicioRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:ServicioRepeater runat=\"server\"></{0}:ServicioRepeater>")]
	public class ServicioRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:ServicioRepeater"/> class.
        /// </summary>
		public ServicioRepeater()
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
		[TemplateContainer(typeof(ServicioItem))]
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
		[TemplateContainer(typeof(ServicioItem))]
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
        [TemplateContainer(typeof(ServicioItem))]
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
		[TemplateContainer(typeof(ServicioItem))]
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
		[TemplateContainer(typeof(ServicioItem))]
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
						MAT.Entities.Servicio entity = o as MAT.Entities.Servicio;
						ServicioItem container = new ServicioItem(entity);
	
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
	public class ServicioItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MAT.Entities.Servicio _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ServicioItem"/> class.
        /// </summary>
		public ServicioItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ServicioItem"/> class.
        /// </summary>
		public ServicioItem(MAT.Entities.Servicio entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the ServicioId
        /// </summary>
        /// <value>The ServicioId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid ServicioId
		{
			get { return _entity.ServicioId; }
		}
        /// <summary>
        /// Gets the Descripcion
        /// </summary>
        /// <value>The Descripcion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Descripcion
		{
			get { return _entity.Descripcion; }
		}
        /// <summary>
        /// Gets the Precio
        /// </summary>
        /// <value>The Precio.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? Precio
		{
			get { return _entity.Precio; }
		}
        /// <summary>
        /// Gets the Moneda
        /// </summary>
        /// <value>The Moneda.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Moneda
		{
			get { return _entity.Moneda; }
		}
        /// <summary>
        /// Gets the Iva
        /// </summary>
        /// <value>The Iva.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Iva
		{
			get { return _entity.Iva; }
		}
        /// <summary>
        /// Gets the Alicuota
        /// </summary>
        /// <value>The Alicuota.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? Alicuota
		{
			get { return _entity.Alicuota; }
		}
        /// <summary>
        /// Gets the Validez
        /// </summary>
        /// <value>The Validez.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Validez
		{
			get { return _entity.Validez; }
		}
        /// <summary>
        /// Gets the VisibilidadTarifa
        /// </summary>
        /// <value>The VisibilidadTarifa.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? VisibilidadTarifa
		{
			get { return _entity.VisibilidadTarifa; }
		}
        /// <summary>
        /// Gets the ProveedorId
        /// </summary>
        /// <value>The ProveedorId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid? ProveedorId
		{
			get { return _entity.ProveedorId; }
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
        /// Gets the HotelId
        /// </summary>
        /// <value>The HotelId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid? HotelId
		{
			get { return _entity.HotelId; }
		}
        /// <summary>
        /// Gets the TipoServicio
        /// </summary>
        /// <value>The TipoServicio.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? TipoServicio
		{
			get { return _entity.TipoServicio; }
		}

        /// <summary>
        /// Gets a <see cref="T:MAT.Entities.Servicio"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MAT.Entities.Servicio Entity
        {
            get { return _entity; }
        }
	}
}
