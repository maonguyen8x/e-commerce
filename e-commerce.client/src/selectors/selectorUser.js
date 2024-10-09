/* eslint-disable no-plusplus */
/* eslint-disable no-else-return */
export const selectFilter = (users, filter) => {
  if (!users || users.length === 0) return [];

  const keyword = filter.keyword.toLowerCase();

  return users.filter((user) => {
    const matchKeyword = user.keywords ? user.keywords.includes(keyword) : true;
    const matchFullName = user.description
      ? user.description.toLowerCase().includes(keyword)
      : true;
    const matchAddress = user.description
    ? user.description.toLowerCase().includes(keyword)
    : true;
    return ((matchKeyword || matchFullName) && matchAddress);
  }).sort((a, b) => {
    if (filter.sortBy === 'name-desc') {
      return a.fullName < b.fullName ? 1 : -1;
    } else if (filter.sortBy === 'name-asc') {
      return a.fullName > b.fullName ? 1 : -1;
    } else if (filter.sortBy === 'name-desc') {
      return a.address < b.address ? 1 : -1;
    } else if (filter.sortBy === 'name-asc') {
      return a.address > b.address ? 1 : -1;
    } else if (filter.sortBy === 'name-desc') {
      return a.email < b.email ? 1 : -1;
    } else if (filter.sortBy === 'name-asc') {
      return a.email > b.email ? 1 : -1;
    } else if (filter.sortBy === 'name-asc') {
        return a.role > b.role ? 1 : -1;
      }
  });
};

