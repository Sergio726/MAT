<%@ Control Language="C#" ClassName="AdicionalFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataAdicionalId" runat="server" Text="Adicional Id:" AssociatedControlID="dataAdicionalId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataAdicionalId" Value='<%# Bind("AdicionalId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMonto" runat="server" Text="Monto:" AssociatedControlID="dataMonto" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMonto" Text='<%# Bind("Monto") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataMonto" runat="server" Display="Dynamic" ControlToValidate="dataMonto" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataMonto" runat="server" Display="Dynamic" ControlToValidate="dataMonto" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDescripcion" runat="server" Display="Dynamic" ControlToValidate="dataDescripcion" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


