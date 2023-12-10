<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="NotaEdit.aspx.cs" Inherits="NotaEdit" Title="Nota Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Nota - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="NotaId" runat="server" DataSourceID="NotaDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/NotaFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/NotaFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Nota not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:NotaDataSource ID="NotaDataSource" runat="server"
			SelectMethod="GetByNotaId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="NotaId" QueryStringField="NotaId" Type="String" />

			</Parameters>
		</data:NotaDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewMovimientoCuenta1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewMovimientoCuenta1_SelectedIndexChanged"			 			 
			DataSourceID="MovimientoCuentaDataSource1"
			DataKeyNames="MovimientoId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_MovimientoCuenta.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Pago Id" DataNavigateUrlFormatString="PagoEdit.aspx?PagoId={0}" DataNavigateUrlFields="PagoId" DataContainer="PagoIdSource" DataTextField="FechaPago" />
				<data:HyperLinkField HeaderText="Factura Id" DataNavigateUrlFormatString="FacturaEdit.aspx?FacturaId={0}" DataNavigateUrlFields="FacturaId" DataContainer="FacturaIdSource" DataTextField="NroFactura" />
				<asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" SortExpression="[FechaRegistro]" />				
				<data:HyperLinkField HeaderText="Cuenta Id" DataNavigateUrlFormatString="CuentaEdit.aspx?CuentaId={0}" DataNavigateUrlFields="CuentaId" DataContainer="CuentaIdSource" DataTextField="Estado" />
				<data:HyperLinkField HeaderText="Nota Id" DataNavigateUrlFormatString="NotaEdit.aspx?NotaId={0}" DataNavigateUrlFields="NotaId" DataContainer="NotaIdSource" DataTextField="PorcentajeRetencion" />
				<data:HyperLinkField HeaderText="Cuenta Corriente Id" DataNavigateUrlFormatString="CuentaCorrienteEdit.aspx?CuentaCorrienteId={0}" DataNavigateUrlFields="CuentaCorrienteId" DataContainer="CuentaCorrienteIdSource" DataTextField="Fecha" />
				<data:HyperLinkField HeaderText="Debito Id" DataNavigateUrlFormatString="DebitoEdit.aspx?DebitoId={0}" DataNavigateUrlFields="DebitoId" DataContainer="DebitoIdSource" DataTextField="Fecha" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Movimiento Cuenta Found! </b>
				<asp:HyperLink runat="server" ID="hypMovimientoCuenta" NavigateUrl="~/admin/MovimientoCuentaEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:MovimientoCuentaDataSource ID="MovimientoCuentaDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:MovimientoCuentaProperty Name="Cuenta"/> 
					<data:MovimientoCuentaProperty Name="CuentaCorriente"/> 
					<data:MovimientoCuentaProperty Name="Debito"/> 
					<data:MovimientoCuentaProperty Name="Factura"/> 
					<data:MovimientoCuentaProperty Name="Nota"/> 
					<data:MovimientoCuentaProperty Name="Pago"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:MovimientoCuentaFilter  Column="NotaId" QueryStringField="NotaId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:MovimientoCuentaDataSource>		
		
		<br />
		

</asp:Content>

