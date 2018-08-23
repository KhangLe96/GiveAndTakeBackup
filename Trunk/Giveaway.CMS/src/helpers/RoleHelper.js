const getCurrentRole = () => {
  const currentUserLS = localStorage.getItem('currentUser');
  const currentUserJson = currentUserLS ? JSON.parse(currentUserLS) : {};
  return currentUserJson
    && currentUserJson.profile
    && currentUserJson.profile.role
    && currentUserJson.profile.role.toLowerCase();
};

const isAdmin = () => {
  const currentRole = getCurrentRole();
  return currentRole && currentRole.toLowerCase() === 'admin';
};

const RoleHelper = {
  getCurrentRole,
  isAdmin,
};
export default RoleHelper;
