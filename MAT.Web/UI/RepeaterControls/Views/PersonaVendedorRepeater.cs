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
    /// A designer class for a strongly typed repeater <c>PersonaVendedorRepeater</c>
    /// </summary>
	public class PersonaVendedorRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:PersonaVendedorRepeaterDesigner"/> class.
        /// </summary>
		public PersonaVendedorRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is PersonaVendedorRepeater))
			{ 
				throw new ArgumentException("Component is not a PersonaVendedorRepeater."); 
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
			PersonaVendedorRepeater z = (PersonaVendedorRepeater)Component;
			z.DataBind();

			return base.GetDesignTimeHtml();
		}
	}

    /// <summary>
    /// A strongly typed repeater control for the <see cref="PersonaVendedorRepeater"/> Type.
    /// </summary>
	[Designer(typeof(PersonaVendedorRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:PersonaVendedorRepeater runat=\"server\"></{0}:PersonaVendedorRepeater>")]
	public class PersonaVendedorRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:PersonaVendedorRepeater"/> class.
        /// </summary>
		public PersonaVendedorRepeater()
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
		[TemplateContainer(typeof(PersonaVendedorItem))]
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
		[TemplateContainer(typeof(PersonaVendedorItem))]
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
        [TemplateContainer(typeof(PersonaVendedorItem))]
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
		[TemplateContainer(typeof(PersonaVendedorItem))]
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
		[TemplateContainer(typeof(PersonaVendedorItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate FooterTemplate
		{
			get { return m_footerTemplate; }
			set { m_footerTemplate = value; }
		}
		
		
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

//      /// <summary>
//      /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
//      /// </summary>
//		protected override void CreateChildControls()
//		{
//			if (ChildControlsCreated)
//			{
//				return;
//			}
//			Controls.Clear();
//
//			if (m_headerTemplate != null)
//			{
//				Control headerItem = new Control();
//				m_headerTemplate.InstantiateIn(headerItem);
//				Controls.Add(headerItem);
//			}
//
//			
//			if (m_footerTemplate != null)
//			{
//				Control footerItem = new Control();
//				m_footerTemplate.InstantiateIn(footerItem);
//				Controls.Add(footerItem);
//			}
//			ChildControlsCreated = true;
//		}
		
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
						MAT.Entities.PersonaVendedor entity = o as MAT.Entities.PersonaVendedor;
						PersonaVendedorItem container = new PersonaVendedorItem(entity);
	
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
	public class PersonaVendedorItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MAT.Entities.PersonaVendedor _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PersonaVendedorItem"/> class.
        /// </summary>
		public PersonaVendedorItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PersonaVendedorItem"/> class.
        /// </summary>
		public PersonaVendedorItem(MAT.Entities.PersonaVendedor entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the PersonaId
        /// </summary>
        /// <value>The PersonaId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid PersonaId
		{
			get { return _entity.PersonaId; }
		}
        /// <summary>
        /// Gets the Apellido
        /// </summary>
        /// <value>The Apellido.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Apellido
		{
			get { return _entity.Apellido; }
		}
        /// <summary>
        /// Gets the Nombre
        /// </summary>
        /// <value>The Nombre.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Nombre
		{
			get { return _entity.Nombre; }
		}
        /// <summary>
        /// Gets the NroDocumento
        /// </summary>
        /// <value>The NroDocumento.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String NroDocumento
		{
			get { return _entity.NroDocumento; }
		}
        /// <summary>
        /// Gets the Domicilio
        /// </summary>
        /// <value>The Domicilio.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Domicilio
		{
			get { return _entity.Domicilio; }
		}
        /// <summary>
        /// Gets the Telefono
        /// </summary>
        /// <value>The Telefono.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Telefono
		{
			get { return _entity.Telefono; }
		}
        /// <summary>
        /// Gets the Email
        /// </summary>
        /// <value>The Email.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Email
		{
			get { return _entity.Email; }
		}
        /// <summary>
        /// Gets the FechaNacimiento
        /// </summary>
        /// <value>The FechaNacimiento.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? FechaNacimiento
		{
			get { return _entity.FechaNacimiento; }
		}
        /// <summary>
        /// Gets the Sexo
        /// </summary>
        /// <value>The Sexo.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Sexo
		{
			get { return _entity.Sexo; }
		}
        /// <summary>
        /// Gets the LocalidadId
        /// </summary>
        /// <value>The LocalidadId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? LocalidadId
		{
			get { return _entity.LocalidadId; }
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
        /// Gets the VendedorId
        /// </summary>
        /// <value>The VendedorId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid VendedorId
		{
			get { return _entity.VendedorId; }
		}
        /// <summary>
        /// Gets the TipoDocumento
        /// </summary>
        /// <value>The TipoDocumento.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? TipoDocumento
		{
			get { return _entity.TipoDocumento; }
		}

	}
}
