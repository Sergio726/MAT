<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Viaje.aspx.cs" Inherits="Viaje" Title="Viaje List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Viaje List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="ViajeDataSource"
				DataKeyNames="ViajeId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Viaje.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="ViajeId" HeaderText="Viaje Id" SortExpression="[ViajeID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
				<asp:BoundField DataField="Origen" HeaderText="Origen" SortExpression="[Origen]"  />
				<asp:BoundField DataField="FechaSalida" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Salida" SortExpression="[FechaSalida]"  />
				<asp:BoundField DataField="HoraSalida" HeaderText="Hora Salida" SortExpression="[HoraSalida]"  />
				<asp:BoundField DataField="PaisOrigen" HeaderText="Pais Origen" SortExpression="[PaisOrigen]"  />
				<asp:BoundField DataField="PaisDestino" HeaderText="Pais Destino" SortExpression="[PaisDestino]"  />
				<asp:BoundField DataField="Paso" HeaderText="Paso" SortExpression="[Paso]"  />
				<asp:BoundField DataField="Medio" HeaderText="Medio" SortExpression="[Medio]"  />
				<data:HyperLinkField HeaderText="Bus Id" DataNavigateUrlFormatString="TransporteEdit.aspx?TransporteId={0}" DataNavigateUrlFields="TransporteId" DataContainer="BusIdSource" DataTextField="NroCoche" />
				<asp:BoundField DataField="FechaRegreso" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Regreso" SortExpression="[FechaRegreso]"  />
				<asp:BoundField DataField="HoraRegreso" HeaderText="Hora Regreso" SortExpression="[HoraRegreso]"  />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]"  />
				<asp:BoundField DataField="PrecioSemicama" HeaderText="Precio Semicama" SortExpression="[PrecioSemicama]"  />
				<asp:BoundField DataField="PrecioCama" HeaderText="Precio Cama" SortExpression="[PrecioCama]"  />
				<asp:BoundField DataField="PrecioPromocional" HeaderText="Precio Promocional" SortExpression="[PrecioPromocional]"  />
				<asp:BoundField DataField="FechaPromocion" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Promocion" SortExpression="[FechaPromocion]"  />
				<asp:BoundField DataField="NDias" HeaderText="N Dias" SortExpression="[nDias]"  />
				<asp:BoundField DataField="NNoches" HeaderText="N Noches" SortExpression="[nNoches]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Viaje Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnViaje" OnClientClick="javascript:location.href='ViajeEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:ViajeDataSource ID="ViajeDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
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
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:ViajeDataSource>
	    		
</asp:Content>



