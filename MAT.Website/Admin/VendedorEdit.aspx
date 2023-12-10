<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="VendedorEdit.aspx.cs" Inherits="VendedorEdit" Title="Vendedor Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Vendedor - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="VendedorId" runat="server" DataSourceID="VendedorDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/VendedorFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/VendedorFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Vendedor not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:VendedorDataSource ID="VendedorDataSource" runat="server"
			SelectMethod="GetByVendedorId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="VendedorId" QueryStringField="VendedorId" Type="String" />

			</Parameters>
		</data:VendedorDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewFactura1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewFactura1_SelectedIndexChanged"			 			 
			DataSourceID="FacturaDataSource1"
			DataKeyNames="FacturaId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Factura.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="NroFactura" HeaderText="Nro Factura" SortExpression="[NroFactura]" />				
				<asp:BoundField DataField="Monto" HeaderText="Monto" SortExpression="[Monto]" />				
				<asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="[Fecha]" />				
				<asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="[Tipo]" />				
				<asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="[Estado]" />				
				<data:HyperLinkField HeaderText="Cliente Id" DataNavigateUrlFormatString="ClienteEdit.aspx?ClienteId={0}" DataNavigateUrlFields="ClienteId" DataContainer="ClienteIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Vendedor Id" DataNavigateUrlFormatString="VendedorEdit.aspx?VendedorId={0}" DataNavigateUrlFields="VendedorId" DataContainer="VendedorIdSource" DataTextField="Descripcion" />
				<asp:BoundField DataField="DescuentoAplicado" HeaderText="Descuento Aplicado" SortExpression="[DescuentoAplicado]" />				
				<asp:BoundField DataField="Observaciones" HeaderText="Observaciones" SortExpression="[Observaciones]" />				
				<asp:BoundField DataField="DiasPreReserva" HeaderText="Dias Pre Reserva" SortExpression="[DiasPreReserva]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Factura Found! </b>
				<asp:HyperLink runat="server" ID="hypFactura" NavigateUrl="~/admin/FacturaEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:FacturaDataSource ID="FacturaDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:FacturaProperty Name="Cliente"/> 
					<data:FacturaProperty Name="Vendedor"/> 
					<%--<data:FacturaProperty Name="MovimientoCuentaCollection" />--%>
					<%--<data:FacturaProperty Name="PasajeCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:FacturaFilter  Column="VendedorId" QueryStringField="VendedorId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:FacturaDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewNota2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewNota2_SelectedIndexChanged"			 			 
			DataSourceID="NotaDataSource2"
			DataKeyNames="NotaId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Nota.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="PorcentajeRetencion" HeaderText="Porcentaje Retencion" SortExpression="[PorcentajeRetencion]" />				
				<asp:BoundField DataField="MontoRetencion" HeaderText="Monto Retencion" SortExpression="[MontoRetencion]" />				
				<asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="[Fecha]" />				
				<asp:BoundField DataField="Dias" HeaderText="Dias" SortExpression="[Dias]" />				
				<data:HyperLinkField HeaderText="Cliente Id" DataNavigateUrlFormatString="ClienteEdit.aspx?ClienteId={0}" DataNavigateUrlFields="ClienteId" DataContainer="ClienteIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Vendedor Id" DataNavigateUrlFormatString="VendedorEdit.aspx?VendedorId={0}" DataNavigateUrlFields="VendedorId" DataContainer="VendedorIdSource" DataTextField="Descripcion" />
				<asp:BoundField DataField="NroNota" HeaderText="Nro Nota" SortExpression="[NroNota]" />				
				<asp:BoundField DataField="MontoNota" HeaderText="Monto Nota" SortExpression="[MontoNota]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Nota Found! </b>
				<asp:HyperLink runat="server" ID="hypNota" NavigateUrl="~/admin/NotaEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:NotaDataSource ID="NotaDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:NotaProperty Name="Cliente"/> 
					<data:NotaProperty Name="Vendedor"/> 
					<%--<data:NotaProperty Name="MovimientoCuentaCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:NotaFilter  Column="VendedorId" QueryStringField="VendedorId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:NotaDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewPago3" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPago3_SelectedIndexChanged"			 			 
			DataSourceID="PagoDataSource3"
			DataKeyNames="PagoId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Pago.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" SortExpression="[FechaPago]" />				
				<asp:BoundField DataField="Monto" HeaderText="Monto" SortExpression="[Monto]" />				
				<asp:BoundField DataField="TipoPago" HeaderText="Tipo Pago" SortExpression="[TipoPago]" />				
				<asp:BoundField DataField="TransaccionId" HeaderText="Transaccion Id" SortExpression="[TransaccionID]" />				
				<data:HyperLinkField HeaderText="Cliente Id" DataNavigateUrlFormatString="ClienteEdit.aspx?ClienteId={0}" DataNavigateUrlFields="ClienteId" DataContainer="ClienteIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Vendedor Id" DataNavigateUrlFormatString="VendedorEdit.aspx?VendedorId={0}" DataNavigateUrlFields="VendedorId" DataContainer="VendedorIdSource" DataTextField="Descripcion" />
				<asp:BoundField DataField="NroRecibo" HeaderText="Nro Recibo" SortExpression="[NroRecibo]" />				
				<asp:BoundField DataField="EstadoRendicion" HeaderText="Estado Rendicion" SortExpression="[EstadoRendicion]" />				
				<data:HyperLinkField HeaderText="Cuenta Corriente Id" DataNavigateUrlFormatString="CuentaCorrienteEdit.aspx?CuentaCorrienteId={0}" DataNavigateUrlFields="CuentaCorrienteId" DataContainer="CuentaCorrienteIdSource" DataTextField="Fecha" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Pago Found! </b>
				<asp:HyperLink runat="server" ID="hypPago" NavigateUrl="~/admin/PagoEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PagoDataSource ID="PagoDataSource3" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PagoProperty Name="Cliente"/> 
					<data:PagoProperty Name="CuentaCorriente"/> 
					<data:PagoProperty Name="Vendedor"/> 
					<%--<data:PagoProperty Name="MovimientoCuentaCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PagoFilter  Column="VendedorId" QueryStringField="VendedorId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PagoDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewVoucher4" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewVoucher4_SelectedIndexChanged"			 			 
			DataSourceID="VoucherDataSource4"
			DataKeyNames="VoucherId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Voucher.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Voucher Id" DataNavigateUrlFormatString="VoucherEdit.aspx?VoucherId={0}" DataNavigateUrlFields="VoucherId" DataContainer="VoucherIdSource" DataTextField="NroVoucher" />
				<asp:BoundField DataField="NroVoucher" HeaderText="Nro Voucher" SortExpression="[NroVoucher]" />				
				<asp:BoundField DataField="FechaEmision" HeaderText="Fecha Emision" SortExpression="[FechaEmision]" />				
				<data:HyperLinkField HeaderText="Vendedor Id" DataNavigateUrlFormatString="VendedorEdit.aspx?VendedorId={0}" DataNavigateUrlFields="VendedorId" DataContainer="VendedorIdSource" DataTextField="Descripcion" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Voucher Found! </b>
				<asp:HyperLink runat="server" ID="hypVoucher" NavigateUrl="~/admin/VoucherEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:VoucherDataSource ID="VoucherDataSource4" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:VoucherProperty Name="Vendedor"/> 
					<data:VoucherProperty Name="Voucher"/> 
					<%--<data:VoucherProperty Name="PasajeCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:VoucherFilter  Column="VendedorId" QueryStringField="VendedorId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:VoucherDataSource>		
		
		<br />
		

</asp:Content>

