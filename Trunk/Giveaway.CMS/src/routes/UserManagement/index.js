import React from 'react';
import { connect } from 'dva';
import UserList from './UserList';
import { Input, Select, Button } from 'antd';

const Search = Input.Search;
const Option = Select.Option;

function handleChange(value) {
  console.log(`selected ${value}`);
}

function handleBlur() {
  console.log('blur');
}

function handleFocus() {
  console.log('focus');
}

@connect(({ modals, userManagement }) => ({
  ...modals, ...userManagement,
}))
export default class index extends React.Component {
  componentDidMount() {
    const { users, dispatch } = this.props;
    if (users.length === 0) {
      dispatch({
        type: 'userManagement/fetchUser',
        payload: {},
      });
    }
  }


  render() {
    const { users } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>User Management</h1>
        </div>
        <div>
          <Search
            placeholder="input search text"
            onSearch={value => console.log(value)}
            style={{ width: 500 }}
            size="large"
          />
          {/* &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; */}
          <Select
            showSearch
            style={{ width: 200 }}
            placeholder="Select type to filter"
            optionFilterProp="children"
            size="large"
            onChange={handleChange}
            onFocus={handleFocus}
            onBlur={handleBlur}
            filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
          >
            <Option value="ex1">ex1</Option>
            <Option value="ex2">ex2</Option>
            <Option value="ex3">ex3</Option>
          </Select>,
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <Button type="primary" size="large" icon="search">Search</Button>
        </div>
        <br />
        <div className="containerBody">
          <UserList
            users={users}
            dispatch={this.props.dispatch}
          />
        </div>
      </div>
    );
  }
}
