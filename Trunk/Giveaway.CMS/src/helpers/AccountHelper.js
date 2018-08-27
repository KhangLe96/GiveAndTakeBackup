const getGender = (inputGener) => {
  if (inputGener) {
    const vnGender = ['Nam', 'Nữ'];
    const enGender = ['Male', 'Female'];
    return vnGender[enGender.indexOf(inputGener)];
  }
};

const getCurrentRole = () => {
  const currentUser = localStorage.getItem('currentUser');
  if (currentUser) {
    const response = JSON.parse(currentUser);
    return response && response.profile && response.profile.role;
  }
  return "";
};

const getActivity = (inputActivity) => {
  // if (inputActivity) {
  //   const vnActivity = ['Đã kích hoạt', 'Chưa kích hoạt'];
  //   const enActivity = ['activated', 'not_activated'];
  //   return vnActivity[enActivity.indexOf(inputActivity)];
  // }
  return inputActivity ? "Đang hoạt động" : "Không hoạt động";
};

const AccountHelper = {
  getGender, getActivity, getCurrentRole,
};

export default AccountHelper;
