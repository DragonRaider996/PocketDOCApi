using MailKit.Net.Smtp;
using MimeKit;
using PocDoctor.Entitiess;
using PocDoctor.Models;
using PocDoctor.TokenAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PocDoctor.Repositiries
{
    public class DatabaseRepo
    {
        private ApplicationContext db;
        private TokenAuthOptions tokenOptions;
        string username = "pocketdoc996@gmail.com";
        string password = "";
        public DatabaseRepo(ApplicationContext con, TokenAuthOptions token)
        {
            db = con;
            tokenOptions = token;
        }

        private string GetToken(string user)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user, "TokenAuth"), new[] { new Claim("EntityID", "1", ClaimValueTypes.Integer) });

            var secrutiyToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
            {
                Issuer = tokenOptions.Issuer,
                Audience = tokenOptions.Audience,
                SigningCredentials = tokenOptions.SigningCredentials,
                Expires = DateTime.UtcNow.AddMinutes(5)
            });

            return handler.WriteToken(secrutiyToken);
        }



        public UserResponse ValidateUser(Login log)
        {
            User user;
            UserResponse response = new UserResponse();
            Token token;
            bool temp = false;
            user = db.User.FirstOrDefault(c => c.Email.Equals(log.Email));
            if (user != null)
            {
                string password = user.Password;
                if (password.Equals(log.Password))
                {
                    temp = true;
                }
                if (temp)
                {
                    if (user.Verified == "true")
                    {
                        token = db.Token.FirstOrDefault(c => c.Uid == user.Uid);
                        if (token != null)
                        {
                            response.UID = token.Uid;
                            response.RefreshToken = token.Token1;
                            response.Role = user.Role;
                            return response;
                        }
                        else
                        {
                            response.UID = user.Uid;
                            response.RefreshToken = GetToken(user.Uname);
                            response.Role = user.Role;
                            token = new Token
                            {
                                Uid = response.UID,
                                Token1 = response.RefreshToken
                            };
                            db.Token.Add(token);
                            db.SaveChanges();
                            return response;
                        }
                    }
                    else
                    {
                        response.RefreshToken = "Email Validation Required";
                        return response;
                    }
                }
                else
                {
                    return null;
                }

            }

            else
            {
                return null;
            }
        }



        public UidwithMessage Register(Register register)
        {
            User temp = db.User.FirstOrDefault(c => c.Email.Equals(register.Email));
            UidwithMessage message;
            string reply;

            if (temp == null)
            {
                User temp1 = db.User.FirstOrDefault(c => c.Uname.Equals(register.UserName));
                if (temp1 == null)
                {
                    var check = new EmailAddressAttribute();
                    if (!check.IsValid(register.Email))
                    {
                        message = new UidwithMessage
                        {
                            Uid = 0,
                            Message = "Invalid Email"
                        };
                        return message;
                    }

                    User user = new User
                    {
                        Uname = register.UserName,
                        Email = register.Email,
                        Password = register.Password,
                        Verified = "false",
                        Role = "User"
                    };

                    db.User.Add(user);
                    db.SaveChanges();

                    reply = SendMail(user);



                    if (reply == "Ok")
                    {
                        message = new UidwithMessage
                        {
                            Uid = user.Uid,
                            Message = reply
                        };
                        return message;
                    }
                    else
                    {
                        message = new UidwithMessage
                        {
                            Uid = 0,
                            Message = reply
                        };
                        db.User.Remove(user);
                        return message;
                    }
                }
                else
                {
                    reply = "Username Already Exists !!!";
                    message = new UidwithMessage
                    {
                        Uid = 0,
                        Message = reply
                    };
                    return message;
                }
            }
            else
            {
                reply = "Email Already Exists !!!";
                message = new UidwithMessage
                {
                    Uid = 0,
                    Message = reply
                };
                return message;
            }


        }

        private string SendMail(User user)
        {
            string emailadd = user.Email;
            string reply;



            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("PocketDoc", "pocketdoc996@gmail.com"));
                message.To.Add(new MailboxAddress("", emailadd));
                message.Subject = "Hello";
                int code = GenerateKey(user.Uid);
                var bodybuiler = new BodyBuilder();
                bodybuiler.HtmlBody = @"Your code is : <b>" + code + "</b>";

                message.Body = bodybuiler.ToMessageBody();



                using (var client = new SmtpClient())
                {

                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(username, password);
                    client.Send(message);
                    client.Disconnect(true);
                    reply = "Ok";
                }
            }
            catch (Exception e)
            {
                reply = "Error: " + e.ToString();
            }
            return reply;
        }


        private int GenerateKey(int id)
        {
            Random random = new Random();
            int key = random.Next(1000, 9999);
            VerifyCode code = new VerifyCode
            {
                Uid = id,
                Code = key,
            };

            db.VerifyCode.Add(code);
            db.SaveChanges();
            return key;
        }


        public UidwithMessage VerifyKey(Verification verify)
        {
            VerifyCode code;
            UidwithMessage ans;
            code = db.VerifyCode.FirstOrDefault(c => c.Uid == verify.Uid && c.Code == verify.Code);
            if (code == null)
            {
                return ans = new UidwithMessage { Uid = verify.Uid,Message = "Null"};
            }
            else
            {
                User user = db.User.FirstOrDefault(c => c.Uid == verify.Uid);
                user.Verified = "true";
                db.User.Update(user);
                db.SaveChanges();
                return ans = new UidwithMessage { Uid = verify.Uid, Message = "True" };
            }
        }

        public string ConvertDoc(int id)
        {
            User user = db.User.FirstOrDefault(c => c.Uid == id);
            if (user == null)
            {
                return "Invalid User ID";
            }
            else
            {
                user.Role = "Doctor";
                db.User.Update(user);
                db.SaveChanges();
                return "Ok";
            }
        }



        public ICollection<MainSymptoms> GetMainSymp()
        {
            return db.MainSymptoms.ToList();
        }

        public ICollection<Symptoms> GetOtherSymptoms(int id)
        {
            var temp = db.DisSymp.Where(c => c.Sid == id).Select(c => c.Did).ToList();

            if (temp.Count == 0)
            {
                return null;
            }

            var selected = db.DisSymp.Where(c => (temp.Contains(c.Did)) && (c.Sid != id)).Select(c => c.Sid).Distinct().ToList();

            ICollection<Symptoms> symptoms = db.Symptoms.Where(c => selected.Contains(c.Sid)).ToList();

            return symptoms;
        }

        public ICollection<Diseases> GetDiseaseBySymptoms(int id, List<int> sympid)
        {
            var temp = db.DisSymp.Where(c => c.Sid == id).Select(c => c.Did).ToList();

            if (temp.Count == 0)
            {
                return null;
            }
            else
            {
                List<int> dis = db.DisSymp.Join(
                    db.DisSymp,
                    c => c.Did,
                    ca => ca.Did,
                    (c, ca) => new { a = c, b = ca }).Where((x => (sympid.Contains(x.a.Sid)) && (x.b.Sid == id))).Select(x => x.a.Did).Distinct().ToList();

                if (dis.Count == 0)
                {
                    return null;
                }

                ICollection<Diseases> disease = db.Diseases.Where(c => dis.Contains(c.Did)).ToList();

                return disease;
            }
        }

        public ICollection<Diseases> GetAllDisease()
        {
            ICollection<Diseases> disease;
            disease = db.Diseases.ToList();
            return disease;
        }


        public DiseaseShow GetSpecificDisease(int id)
        {
            DiseaseShow show;
            Diseases disease = db.Diseases.FirstOrDefault(c => c.Did == id);
            if (disease == null)
            {
                return null;
            }
            else
            {
                var temp = db.DisSymp.Where(c => c.Did == id).Select(c => c.Sid).ToList();
                ICollection<Symptoms> symptoms = db.Symptoms.Where(c => temp.Contains(c.Sid)).ToList();

                var temp1 = db.DisPrev.Where(c => c.Did == id).Select(c => c.Pid).ToList();
                ICollection<Prevention> prevent = db.Prevention.Where(c => temp1.Contains(c.Pid)).ToList();

                var temp2 = db.DisRem.Where(c => c.Did == id).Select(c => c.Rid).ToList();
                ICollection<Remedies> rem = db.Remedies.Where(c => temp2.Contains(c.Rid)).ToList();

                show = new DiseaseShow
                {
                    disease = disease,
                    symptoms = symptoms,
                    prevention = prevent,
                    remedies = rem
                };

                return show;
            }
        }


        public List<Hospital> HospByArea(string area)
        {
            List<Hospital> hosp;
            hosp = db.Hospital.Where(c => c.Harea == area).ToList();

            if (hosp == null)
            {
                return null;
            }
            else
            {
                return hosp;
            }
        }


        public List<FirstAidQues> FirstAidTopic()
        {
            List<FirstAidQues> topic;
            topic = db.FirstAidQues.ToList();
            return topic;
        }


        public FirstAidData GetFirstAid(int i)
        {
            var temp = db.FirstAidRel.Where(c => c.Faid == i).Select(c => c.Aid).ToList();

            if (temp == null)
            {
                return null;
            }

            List<FirstAidAns> answer = db.FirstAidAns.Where(c => temp.Contains(c.Aid)).ToList();

            FirstAidQues top = db.FirstAidQues.FirstOrDefault(c => c.Faid == i);

            FirstAidData data = new FirstAidData()
            {
                Topic = top,
                Ans = answer
            };

            return data;
        }


        public List<ForumData> GetAllForum()
        {
            List<ForumData> data = new List<ForumData>();

            List<Forum> forum = db.Forum.ToList();

            for (int i = 0; i < forum.Count; i++)
            {
                ForumData temp = new ForumData
                {
                    Fid = forum[i].Fid,
                    Uid = forum[i].Uid,
                    Question = forum[i].Question,
                    Description = forum[i].Description
                };
                data.Add(temp);
            }
            return data;

        }

        public SpecificForum GetSpecificForum(int id)
        {
            Forum forum = db.Forum.FirstOrDefault(c=>c.Fid == id);
            List<ForumComments> comments = db.ForumComments.Where(c => c.Fid == id).ToList();

            List<Comments> comm = new List<Comments>();

            if (forum == null)
            {
                return null;
            }

            for (int i = 0; i < comments.Count; i++)
            {
                string comment = comments[i].Comments;
                int uid = comments[i].Uid;
                string name = db.User.Where(c => c.Uid == uid).Select(c=>c.Uname).FirstOrDefault();

                Comments com = new Comments {
                    Comment = comment,
                    Name = name
                };

                comm.Add(com);
            }

            SpecificForum specif = new SpecificForum {
                Fid = forum.Fid,
                Question = forum.Question,
                Description = forum.Description,
                Comments = comm
            };

            return specif;
        }

        public string PostData(ForumData data)
        {
            User user = db.User.FirstOrDefault(c => c.Uid == data.Uid);

            if (user == null)
            {
                return "Invalid User";
            }
            else {
                Forum forum = new Forum
                {
                    Uid = data.Uid,
                    Question = data.Question,
                    Description = data.Description,
                };
                db.Forum.Add(forum);
                db.SaveChanges();
                return "Success";
            }
        }

        public string PostComment(CommentPost data)
        {
            User user = db.User.FirstOrDefault(c => c.Uid == data.Uid);
            if (user == null)
            {
                return "Invalid User ID";
            }

            Forum forum = db.Forum.FirstOrDefault(c => c.Fid == data.Fid);
            if (forum == null)
            {
                return "Invalid Question";
            }

            if (user.Role == "User")
            {
                return "Invalid User";
            }
            ForumComments comment = new ForumComments {
                Uid = data.Uid,
                Fid = data.Fid,
                Comments = data.Comments,
            };
            db.ForumComments.Add(comment);
            db.SaveChanges();
            return "Ok";
        }


        public List<HumanAnatomy> GetHumanAnatomy()
        {
            List<HumanAnatomy> data;

            data = db.HumanAnatomy.ToList();

            return data;

        }

        public Anatomy GetSpecificAnatomy(int id)
        {
            Anatomy data;

            HumanAnatomy anatom = db.HumanAnatomy.FirstOrDefault(c => c.Haid == id);

            if (anatom == null)
            {
                return null;
            }

            var temp = db.HumanRelation.Where(c => c.Haid == id).Select(c => c.Hapid);

            List<HumanPara> para = db.HumanPara.Where(c => temp.Contains(c.Hapid)).ToList();

            List<HumanPicture> pic = db.HumanPicture.Where(c => c.Haid == id).ToList();

            List<string> paradata =new List<string>();

            List<string> imagedata = new List<string>();

            for (int i = 0; i < para.Count; i++)
            {
                string par = para[i].Paragraph;
                paradata.Add(par);
            }

            for(int j=0;j<pic.Count;j++)
            {
                string image = pic[j].Image;
                imagedata.Add(image);
            }

            data = new Anatomy {
                Name = anatom.Hatopic,
                Paragraph = paradata,
                Images = imagedata,
            };

            return data;
        }

    }
}
