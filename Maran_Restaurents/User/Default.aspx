<%@ Page Title="" Language="C#" MasterPageFile="~/User/user.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Maran_Restaurents.User.Default" %>
<%@ Import Namespace="Maran_Restaurents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- offer section -->

    <section class="offer_section layout_padding-bottom">
        <div class="offer_container">
            <div class="container ">
                <div class="row">
                    <asp:Repeater ID="rep_discount" runat="server">
                        <ItemTemplate>
                            <div class="col-md-6  ">
                                <div class="box ">
                                    <div class="img-box">
                                        <a href="Menu.aspx?id=<%# Eval("Categories_id") %>">
                                        <img src="<%# Utils.GetImageURL(Eval("ImageUrl")) %> " alt="">
                                        </a>
                                    </div>
                                    <div class="detail-box">
                                        <h5> <%#Eval("cate_Name") %>
                                        </h5>
                                        <h6>
                                            <span>20%</span> Off
                                        </h6>
                                        <a href="Menu.aspx?id=<%# Eval("Categories_id") %>">Order Now
                                    <svg version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 456.029 456.029" style="enable-background:new 0 0 456,029 456,029;" xml:space="preserve">
                                        <g>
                                            <g>
                                                <path d="M345.6,338.862c-29.184,0-53.248,23.552-53.248,53.248c0,29.184,23.552,53.248,53.248,53.248
                   c29.184,0,53.248-23.552,53.248-53.248C398.336,362.926,374.784,338.862,345.6,338.862z" />
                                            </g>
                                        </g>
                                        <g>
                                            <g>
                                                <path d="M439.296,84.91c-1.024,0-2.56-0.512-4.096-0.512H112.64l-5.12-34.304C104.448,27.566,84.992,10.67,61.952,10.67H20.48
                   C9.216,10.67,0,19.886,0,31.15c0,11.264,9.216,20.48,20.48,20.48h41.472c2.56,0,4.608,2.048,5.12,4.608l31.744,216.064
                   c4.096,27.136,27.648,47.616,55.296,47.616h212.992c26.624,0,49.664-18.944,55.296-45.056l33.28-166.4
                   C457.728,97.71,450.56,86.958,439.296,84.91z" />
                                            </g>
                                        </g>
                                        <g>
                                            <g>
                                                <path d="M215.04,389.55c-1.024-28.16-24.576-50.688-52.736-50.688c-29.696,1.536-52.224,26.112-51.2,55.296
                   c1.024,28.16,24.064,50.688,52.224,50.688h1.024C193.536,443.31,216.576,418.734,215.04,389.55z" />
                                            </g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                        <g>
                                        </g>
                                    </svg>
                                        </a>
                                    </div>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>

    <!-- end offer section -->

    <!-- about section -->

   <section class="about_section layout_padding">
     <div class="container  ">

         <div class="row">
             <div class="col-md-6 ">
                 <div class="img-box">
                     <img src="../TemplateFiles/images/about-img1.jpg" alt="">
                 </div>
             </div>
             <div class="col-md-6">
                 <div class="detail-box">
                     <div class="heading_container">
                         <h2>Our Specialities
                         </h2>
                     </div>
                     <p>
                         Global Flavors, Street Food Vibes: Our culinary expertise transcends borders, bringing you the best of both worlds. Experience the vibrant energy of street food, priced just right, while savoring flavors that meet international standards. Our commitment to excellence extends beyond the taste to the very ambiance you dine in. Immerse yourself in an international setting that complements our delectable offerings, ensuring a dining experience that leaves an indelible mark
                     </p>
                     <p>
                         Pizza and Burgers Elevated to Perfection: We've elevated the classic favorites, pizza, and burgers, to an art form. Our speciality is crafting these iconic dishes with an unwavering commitment to taste and quality. Each bite of our pizza boasts a symphony of flavors, from the perfect crust to the harmonious blend of toppings. Our burgers, meanwhile, are a masterpiece of juicy perfection, with every layer thoughtfully composed for a taste that's simply unforgettable.
                     </p>
                     <p>
                         Savor the Extraordinary at Affordable Prices: At Hungry Point, we believe that exceptional food doesn't have to come with a hefty price tag. Our speciality lies in offering awesome, mouthwatering dishes that delight your taste buds without breaking the bank. We take pride in curating a diverse menu of delectable treats that combine remarkable flavors with reasonable prices, ensuring that everyone can experience a culinary journey like no other.
                     </p>

                     <%--<a href="">
           Read More
         </a>--%>
                 </div>
             </div>
         </div>
     </div>
 </section>

    <!-- end about section -->


</asp:Content>
