<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Pasaje.aspx.cs" Inherits="Pasaje" Title="Pasaje List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Pasaje List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PasajeDataSource"
				DataKeyNames="PasajeId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Pasaje.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PasajeId" HeaderText="Pasaje Id" SortExpression="[PasajeID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Pasajero Id" DataNavigateUrlFormatString="PasajeroEdit.aspx?PasajeroId={0}" DataNavigateUrlFields="PasajeroId" DataContainer="PasajeroIdSource" DataTextField="Pasaporte" />
				<data:HyperLinkField HeaderText="Butaca Id" DataNavigateUrlFormatString="ButacaEdit.aspx?ButacaId={0}" DataNavigateUrlFields="ButacaId" DataContainer="ButacaIdSource" DataTextField="NroButaca" />
				<asp:BoundField DataField="FechaReserva" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Reserva" SortExpression="[FechaReserva]"  />
				<asp:BoundField DataField="FechaCompra" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Compra" SortExpression="[FechaCompra]"  />
				<data:HyperLinkField HeaderText="Viaje Id" DataNavigateUrlFormatString="ViajeEdit.aspx?ViajeId={0}" DataNavigateUrlFields="ViajeId" DataContainer="ViajeIdSource" DataTextField="Origen" />
				<data:HyperLinkField HeaderText="Factura Id" DataNavigateUrlFormatString="FacturaEdit.aspx?FacturaId={0}" DataNavigateUrlFields="FacturaId" DataContainer="FacturaIdSource" DataTextField="NroFactura" />
				<data:HyperLinkField HeaderText="Estado Pasaje" DataNavigateUrlFormatString="EstadoPasajeEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="EstadoPasajeSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Voucher Id" DataNavigateUrlFormatString="VoucherEdit.aspx?VoucherId={0}" DataNavigateUrlFields="VoucherId" DataContainer="VoucherIdSource" DataTextField="NroVoucher" />
				<data:HyperLinkField HeaderText="Precio Id" DataNavigateUrlFormatString="PrecioEdit.aspx?PrecioId={0}" DataNavigateUrlFields="PrecioId" DataContainer="PrecioIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Pasaje Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPasaje" OnClientClick="javascript:location.href='PasajeEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PasajeDataSource ID="PasajeDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PasajeProperty Name="Butaca"/> 
					<data:PasajeProperty Name="Factura"/> 
					<data:PasajeProperty Name="Pasajero"/> 
					<data:PasajeProperty Name="Precio"/> 
					<data:PasajeProperty Name="Viaje"/> 
					<data:PasajeProperty Name="Voucher"/> 
					<data:PasajeProperty Name="EstadoPasaje"/> 
					<%--<data:PasajeProperty Name="PasajeAdicionalCollection" />--%>
					<%--<data:PasajeProperty Name="ReservaHabitacionCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PasajeDataSource>
	    		
</asp:Content>



