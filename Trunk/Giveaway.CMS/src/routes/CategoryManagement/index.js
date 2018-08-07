import React from 'react';
import { connect } from 'dva';
import CategoryList from '../CategoryManagement/CategoryList';

@connect(({ modals, categoryManagement }) => ({
  ...modals, ...categoryManagement,
}))
export default class index extends React.Component {
  componentDidMount() {
    const { categories, dispatch } = this.props;
    if (categories.length === 0) {
      dispatch({
        type: 'categoryManagement/getCategories',
        payload: {},
      });
    }
  }

  render() {
    const { categories, currentPage, dispatch, totals } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý danh mục</h1>
        </div>
        <div className="containerBody">
          <CategoryList
            categories={categories}
            currentPage={currentPage}
            dispatch={dispatch}
            totals={totals}
          />
        </div>
      </div>
    );
  }
}
index.defaultProps = { categories: [], totals: 0 };
