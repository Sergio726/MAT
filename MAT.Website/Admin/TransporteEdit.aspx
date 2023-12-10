<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="TransporteEdit.aspx.cs" Inherits="TransporteEdit" Title="Transporte Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Transporte - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="TransporteId" runat="server" DataSourceID="TransporteDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/TransporteFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/TransporteFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Transporte not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:TransporteDataSource ID="TransporteDataSource" runat="server"
			SelectMethod="GetByTransporteId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="TransporteId" QueryStringField="TransporteId" Type="String" />

			</Parameters>
		</data:TransporteDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewViaje1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewViaje1_SelectedIndexChanged"			 			 
			DataSourceID="ViajeDataSource1"
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
		
		<data:ViajeDataSource ID="ViajeDataSource1" runat="server" SelectMethod="Find"
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
						<data:ViajeFilter  Column="BusId" QueryStringField="TransporteId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ViajeDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewServicio2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewServicio2_SelectedIndexChanged"			 			 
			DataSourceID="ServicioDataSource2"
			DataKeyNames="ServicioId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Servicio.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]" />				
				<asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="[Precio]" />				
				<asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="[Moneda]" />				
				<asp:BoundField DataField="Iva" HeaderText="Iva" SortExpression="[Iva]" />				
				<asp:BoundField DataField="Alicuota" HeaderText="Alicuota" SortExpression="[Alicuota]" />				
				<asp:BoundField DataField="Validez" HeaderText="Validez" SortExpression="[Validez]" />				
				<asp:BoundField DataField="VisibilidadTarifa" HeaderText="Visibilidad Tarifa" SortExpression="[VisibilidadTarifa]" />				
				<data:HyperLinkField HeaderText="Proveedor Id" DataNavigateUrlFormatString="ProveedorEdit.aspx?ProveedorId={0}" DataNavigateUrlFields="ProveedorId" DataContainer="ProveedorIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Transporte Id" DataNavigateUrlFormatString="TransporteEdit.aspx?TransporteId={0}" DataNavigateUrlFields="TransporteId" DataContainer="TransporteIdSource" DataTextField="NroCoche" />
				<data:HyperLinkField HeaderText="Hotel Id" DataNavigateUrlFormatString="HotelEdit.aspx?HotelId={0}" DataNavigateUrlFields="HotelId" DataContainer="HotelIdSource" DataTextField="Nombre" />
				<asp:BoundField DataField="TipoServicio" HeaderText="Tipo Servicio" SortExpression="[TipoServicio]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Servicio Found! </b>
				<asp:HyperLink runat="server" ID="hypServicio" NavigateUrl="~/admin/ServicioEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:ServicioDataSource ID="ServicioDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ServicioProperty Name="Hotel"/> 
					<data:ServicioProperty Name="Proveedor"/> 
					<data:ServicioProperty Name="Transporte"/> 
					<%--<data:ServicioProperty Name="PaqueteServicioCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:ServicioFilter  Column="TransporteId" QueryStringField="TransporteId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ServicioDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewButaca3" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewButaca3_SelectedIndexChanged"			 			 
			DataSourceID="ButacaDataSource3"
			DataKeyNames="ButacaId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Butaca.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="NroButaca" HeaderText="Nro Butaca" SortExpression="[NroButaca]" />				
				<asp:BoundField DataField="Piso" HeaderText="Piso" SortExpression="[Piso]" />				
				<asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="[Ubicacion]" />				
				<asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="[Tipo]" />				
				<data:HyperLinkField HeaderText="Transporte Id" DataNavigateUrlFormatString="TransporteEdit.aspx?TransporteId={0}" DataNavigateUrlFields="TransporteId" DataContainer="TransporteIdSource" DataTextField="NroCoche" />
				<asp:BoundField DataField="Fila" HeaderText="Fila" SortExpression="[Fila]" />				
				<asp:BoundField DataField="Posicion" HeaderText="Posicion" SortExpression="[Posicion]" />				
				<asp:BoundField DataField="CodigoButaca" HeaderText="Codigo Butaca" SortExpression="[CodigoButaca]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Butaca Found! </b>
				<asp:HyperLink runat="server" ID="hypButaca" NavigateUrl="~/admin/ButacaEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:ButacaDataSource ID="ButacaDataSource3" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ButacaProperty Name="Transporte"/> 
					<%--<data:ButacaProperty Name="PasajeCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:ButacaFilter  Column="TransporteId" QueryStringField="TransporteId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ButacaDataSource>		
		
		<br />
		

</asp:Content>

