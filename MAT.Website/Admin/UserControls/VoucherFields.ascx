<%@ Control Language="C#" ClassName="VoucherFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataVoucherId" runat="server" Text="Voucher Id:" AssociatedControlID="dataVoucherId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataVoucherId" DataSourceID="VoucherIdVoucherDataSource" DataTextField="NroVoucher" DataValueField="VoucherId" SelectedValue='<%# Bind("VoucherId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:VoucherDataSource ID="VoucherIdVoucherDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaEmision" runat="server" Text="Fecha Emision:" AssociatedControlID="dataFechaEmision" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaEmision" Text='<%# Bind("FechaEmision", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaEmision" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVendedorId" runat="server" Text="Vendedor Id:" AssociatedControlID="dataVendedorId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataVendedorId" DataSourceID="VendedorIdVendedorDataSource" DataTextField="Descripcion" DataValueField="VendedorId" SelectedValue='<%# Bind("VendedorId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:VendedorDataSource ID="VendedorIdVendedorDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


