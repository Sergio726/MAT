<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PaqueteEdit.aspx.cs" Inherits="PaqueteEdit" Title="Paquete Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Paquete - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PaqueteId" runat="server" DataSourceID="PaqueteDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PaqueteFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PaqueteFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Paquete not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PaqueteDataSource ID="PaqueteDataSource" runat="server"
			SelectMethod="GetByPaqueteId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PaqueteId" QueryStringField="PaqueteId" Type="String" />

			</Parameters>
		</data:PaqueteDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewPaquetePrecio1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPaquetePrecio1_SelectedIndexChanged"			 			 
			DataSourceID="PaquetePrecioDataSource1"
			DataKeyNames="PaquetePrecioId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PaquetePrecio.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Precio Id" DataNavigateUrlFormatString="PrecioEdit.aspx?PrecioId={0}" DataNavigateUrlFields="PrecioId" DataContainer="PrecioIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Precio Found! </b>
				<asp:HyperLink runat="server" ID="hypPaquetePrecio" NavigateUrl="~/admin/PaquetePrecioEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PaquetePrecioDataSource ID="PaquetePrecioDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaquetePrecioProperty Name="Paquete"/> 
					<data:PaquetePrecioProperty Name="Precio"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PaquetePrecioFilter  Column="PaqueteId" QueryStringField="PaqueteId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PaquetePrecioDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewPaqueteAdicional2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPaqueteAdicional2_SelectedIndexChanged"			 			 
			DataSourceID="PaqueteAdicionalDataSource2"
			DataKeyNames="PaqueteAdicionalId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PaqueteAdicional.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Adicional Id" DataNavigateUrlFormatString="AdicionalEdit.aspx?AdicionalId={0}" DataNavigateUrlFields="AdicionalId" DataContainer="AdicionalIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Adicional Found! </b>
				<asp:HyperLink runat="server" ID="hypPaqueteAdicional" NavigateUrl="~/admin/PaqueteAdicionalEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PaqueteAdicionalDataSource ID="PaqueteAdicionalDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaqueteAdicionalProperty Name="Adicional"/> 
					<data:PaqueteAdicionalProperty Name="Paquete"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PaqueteAdicionalFilter  Column="PaqueteId" QueryStringField="PaqueteId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PaqueteAdicionalDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewPaqueteExcursion3" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPaqueteExcursion3_SelectedIndexChanged"			 			 
			DataSourceID="PaqueteExcursionDataSource3"
			DataKeyNames="PaqueteExcursionId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PaqueteExcursion.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Excursion Id" DataNavigateUrlFormatString="ExcursionEdit.aspx?ExcursionId={0}" DataNavigateUrlFields="ExcursionId" DataContainer="ExcursionIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Excursion Found! </b>
				<asp:HyperLink runat="server" ID="hypPaqueteExcursion" NavigateUrl="~/admin/PaqueteExcursionEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PaqueteExcursionDataSource ID="PaqueteExcursionDataSource3" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaqueteExcursionProperty Name="Excursion"/> 
					<data:PaqueteExcursionProperty Name="Paquete"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PaqueteExcursionFilter  Column="PaqueteId" QueryStringField="PaqueteId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PaqueteExcursionDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewViaje4" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewViaje4_SelectedIndexChanged"			 			 
			DataSourceID="ViajeDataSource4"
			DataKeyNames="ViajeId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Viaje.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
				<asp:BoundField DataField="Origen" HeaderText="Origen" SortExpression="[Origen]" />				
				<asp:BoundField DataField="FechaSalida" HeaderText="Fecha Salida" SortExpression="[FechaSalida]" />				
				<asp:BoundField DataField="HoraSalida" HeaderText="Hora Salida" SortExpression="[HoraSalida]" />				
				<asp:BoundField DataField="PaisOrigen" HeaderText="Pais Origen" SortExpression="[PaisOrigen]" />				
				<asp:BoundField DataField="PaisDestino" HeaderText="Pais Destino" SortExpression="[PaisDestino]" />				
				<asp:BoundField DataField="Paso" HeaderText="Paso" SortExpression="[Paso]" />				
				<asp:BoundField DataField="Medio" HeaderText="Medio" SortExpression="[Medio]" />				
				<data:HyperLinkField HeaderText="Bus Id" DataNavigateUrlFormatString="TransporteEdit.aspx?TransporteId={0}" DataNavigateUrlFields="TransporteId" DataContainer="BusIdSource" DataTextField="NroCoche" />
				<asp:BoundField DataField="FechaRegreso" HeaderText="Fecha Regreso" SortExpression="[FechaRegreso]" />				
				<asp:BoundField DataField="HoraRegreso" HeaderText="Hora Regreso" SortExpression="[HoraRegreso]" />				
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]" />				
				<asp:BoundField DataField="PrecioSemicama" HeaderText="Precio Semicama" SortExpression="[PrecioSemicama]" />				
				<asp:BoundField DataField="PrecioCama" HeaderText="Precio Cama" SortExpression="[PrecioCama]" />				
				<asp:BoundField DataField="PrecioPromocional" HeaderText="Precio Promocional" SortExpression="[PrecioPromocional]" />				
				<asp:BoundField DataField="FechaPromocion" HeaderText="Fecha Promocion" SortExpression="[FechaPromocion]" />				
				<asp:BoundField DataField="NDias" HeaderText="N Dias" SortExpression="[nDias]" />				
				<asp:BoundField DataField="NNoches" HeaderText="N Noches" SortExpression="[nNoches]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Viaje Found! </b>
				<asp:HyperLink runat="server" ID="hypViaje" NavigateUrl="~/admin/ViajeEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:ViajeDataSource ID="ViajeDataSource4" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ViajeProperty Name="Paquete"/> 
					<data:ViajeProperty Name="Transporte"/> 
					<%--<data:ViajeProperty Name="PasajeCollection" />--%>
					<%--<data:ViajeProperty Name="ViajeHotelCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:ViajeFilter  Column="PaqueteId" QueryStringField="PaqueteId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ViajeDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewPaqueteServicio5" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPaqueteServicio5_SelectedIndexChanged"			 			 
			DataSourceID="PaqueteServicioDataSource5"
			DataKeyNames="PaqueteServicioId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PaqueteServicio.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Servicio Id" DataNavigateUrlFormatString="ServicioEdit.aspx?ServicioId={0}" DataNavigateUrlFields="ServicioId" DataContainer="ServicioIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Servicio Found! </b>
				<asp:HyperLink runat="server" ID="hypPaqueteServicio" NavigateUrl="~/admin/PaqueteServicioEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PaqueteServicioDataSource ID="PaqueteServicioDataSource5" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaqueteServicioProperty Name="Paquete"/> 
					<data:PaqueteServicioProperty Name="Servicio"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PaqueteServicioFilter  Column="PaqueteId" QueryStringField="PaqueteId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PaqueteServicioDataSource>		
		
		<br />
		

</asp:Content>

