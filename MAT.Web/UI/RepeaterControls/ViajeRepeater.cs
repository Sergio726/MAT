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
    /// A designer class for a strongly typed repeater <c>ViajeRepeater</c>
    /// </summary>
	public class ViajeRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:ViajeRepeaterDesigner"/> class.
        /// </summary>
		public ViajeRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is ViajeRepeater))
			{ 
				throw new ArgumentException("Component is not a ViajeRepeater."); 
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
			ViajeRepeater z = (ViajeRepeater)Component;
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
    /// A strongly typed repeater control for the <see cref="ViajeRepeater"/> Type.
    /// </summary>
	[Designer(typeof(ViajeRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:ViajeRepeater runat=\"server\"></{0}:ViajeRepeater>")]
	public class ViajeRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:ViajeRepeater"/> class.
        /// </summary>
		public ViajeRepeater()
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
		[TemplateContainer(typeof(ViajeItem))]
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
		[TemplateContainer(typeof(ViajeItem))]
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
        [TemplateContainer(typeof(ViajeItem))]
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
		[TemplateContainer(typeof(ViajeItem))]
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
		[TemplateContainer(typeof(ViajeItem))]
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
						MAT.Entities.Viaje entity = o as MAT.Entities.Viaje;
						ViajeItem container = new ViajeItem(entity);
	
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
	public class ViajeItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MAT.Entities.Viaje _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ViajeItem"/> class.
        /// </summary>
		public ViajeItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ViajeItem"/> class.
        /// </summary>
		public ViajeItem(MAT.Entities.Viaje entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the ViajeId
        /// </summary>
        /// <value>The ViajeId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid ViajeId
		{
			get { return _entity.ViajeId; }
		}
        /// <summary>
        /// Gets the PaqueteId
        /// </summary>
        /// <value>The PaqueteId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid? PaqueteId
		{
			get { return _entity.PaqueteId; }
		}
        /// <summary>
        /// Gets the Origen
        /// </summary>
        /// <value>The Origen.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Origen
		{
			get { return _entity.Origen; }
		}
        /// <summary>
        /// Gets the FechaSalida
        /// </summary>
        /// <value>The FechaSalida.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? FechaSalida
		{
			get { return _entity.FechaSalida; }
		}
        /// <summary>
        /// Gets the HoraSalida
        /// </summary>
        /// <value>The HoraSalida.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String HoraSalida
		{
			get { return _entity.HoraSalida; }
		}
        /// <summary>
        /// Gets the PaisOrigen
        /// </summary>
        /// <value>The PaisOrigen.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String PaisOrigen
		{
			get { return _entity.PaisOrigen; }
		}
        /// <summary>
        /// Gets the PaisDestino
        /// </summary>
        /// <value>The PaisDestino.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String PaisDestino
		{
			get { return _entity.PaisDestino; }
		}
        /// <summary>
        /// Gets the Paso
        /// </summary>
        /// <value>The Paso.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Paso
		{
			get { return _entity.Paso; }
		}
        /// <summary>
        /// Gets the Medio
        /// </summary>
        /// <value>The Medio.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Medio
		{
			get { return _entity.Medio; }
		}
        /// <summary>
        /// Gets the BusId
        /// </summary>
        /// <value>The BusId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid? BusId
		{
			get { return _entity.BusId; }
		}
        /// <summary>
        /// Gets the FechaRegreso
        /// </summary>
        /// <value>The FechaRegreso.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? FechaRegreso
		{
			get { return _entity.FechaRegreso; }
		}
        /// <summary>
        /// Gets the HoraRegreso
        /// </summary>
        /// <value>The HoraRegreso.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String HoraRegreso
		{
			get { return _entity.HoraRegreso; }
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
        /// Gets the PrecioSemicama
        /// </summary>
        /// <value>The PrecioSemicama.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? PrecioSemicama
		{
			get { return _entity.PrecioSemicama; }
		}
        /// <summary>
        /// Gets the PrecioCama
        /// </summary>
        /// <value>The PrecioCama.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? PrecioCama
		{
			get { return _entity.PrecioCama; }
		}
        /// <summary>
        /// Gets the PrecioPromocional
        /// </summary>
        /// <value>The PrecioPromocional.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? PrecioPromocional
		{
			get { return _entity.PrecioPromocional; }
		}
        /// <summary>
        /// Gets the FechaPromocion
        /// </summary>
        /// <value>The FechaPromocion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? FechaPromocion
		{
			get { return _entity.FechaPromocion; }
		}
        /// <summary>
        /// Gets the NDias
        /// </summary>
        /// <value>The NDias.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? NDias
		{
			get { return _entity.NDias; }
		}
        /// <summary>
        /// Gets the NNoches
        /// </summary>
        /// <value>The NNoches.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? NNoches
		{
			get { return _entity.NNoches; }
		}

        /// <summary>
        /// Gets a <see cref="T:MAT.Entities.Viaje"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MAT.Entities.Viaje Entity
        {
            get { return _entity; }
        }
	}
}
