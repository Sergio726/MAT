<%@ Control Language="C#" ClassName="PrecioFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioId" runat="server" Text="Precio Id:" AssociatedControlID="dataPrecioId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPrecioId" Value='<%# Bind("PrecioId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMonto" runat="server" Text="Monto:" AssociatedControlID="dataMonto" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMonto" Text='<%# Bind("Monto") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataMonto" runat="server" Display="Dynamic" ControlToValidate="dataMonto" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataMonto" runat="server" Display="Dynamic" ControlToValidate="dataMonto" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVigencia" runat="server" Text="Vigencia:" AssociatedControlID="dataVigencia" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataVigencia" Text='<%# Bind("Vigencia", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataVigencia" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>' MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDescripcion" runat="server" Display="Dynamic" ControlToValidate="dataDescripcion" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMes" runat="server" Text="Mes:" AssociatedControlID="dataMes" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMes" Text='<%# Bind("Mes") %>' MaxLength="100"></asp:TextBox>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


